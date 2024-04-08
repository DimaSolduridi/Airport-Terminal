import { useState, useEffect } from 'react';
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

function Log() {
  const [logs, setLogs] = useState([]);
  const [flightColors, setFlightColors] = useState({}); 

  useEffect(() => {
    const conn = new HubConnectionBuilder()
      .withUrl("https://localhost:7151/airport")
      .configureLogging(LogLevel.Information)
      .build();

    conn.on("UpdateLogs", (log) => {
      console.log("Received log:", log);
      setLogs((prevLogs) => [...prevLogs, log]);
      
      if (!flightColors[log.flight.id]) {
        const newColor = getRandomColor(); 
        setFlightColors((prevColors) => ({
          ...prevColors,
          [log.flight.id]: newColor
        }));
      }


      const logContainer = document.getElementById('logContainer');
            if (logContainer) {
              logContainer.scrollBottom = logContainer.scrollBottom;
            }

    });

    
    conn.start()
      .then(() => console.log("SignalR connected"))
      .catch(err => console.error("SignalR connection error:", err));

    return () => {
      conn.stop();
    };


  }, [flightColors]); 


  const getRandomColor = () => {
    return '#' + Math.floor(Math.random() * 16777215).toString(16);
  };

  const getColorForFlight = (flightId) => {
    return flightColors[flightId] || '#000'; 
  };

  const truncateTimestamp = timestamp => timestamp.slice(0, -12);

  return (
<div id="logContainer" >
  
    {logs.map((log, index) => (
        <div key={index} style={{ backgroundColor: getColorForFlight(log.flight.id), border: '1px solid black', marginBottom: '10px', padding: '5px' }}>
            {log.exitLeg ? (
                <div>
                    Out time: {truncateTimestamp(log.exitLeg)} <br />
                    FOR FLIGHT: {log.flight.number} <br />
                    FROM LEG Leg Number: {log.leg.number}
                </div>
            ) : (
                <div>
                    Flight Code: {log.flight.number} <br />
                    Leg Number: {log.leg.number} <br />
                    In time: {truncateTimestamp(log.enterLeg)} <br />
                    Crossing time: {log.leg.crossingTime} <br />
                </div>
            )}
        </div>
    ))}
</div>
  );
}

export default Log;
