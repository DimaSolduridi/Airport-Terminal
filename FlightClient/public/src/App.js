import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Container, Row, Col } from 'react-bootstrap';
import Log from './Component/logs';
function App() {
  return (
    <div className="App">
      <main>
        <Container>
          <Row className='px-5 my-5'>
            <Col sm='12'>
              <h1>My log:</h1>
              <Log />
            </Col>
          </Row>
        </Container>

      </main>
    </div>
  );
}

export default App;
