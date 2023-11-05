import React, { useState, useEffect } from "react";
import { Container, Row, Col, Tabs, Tab } from "react-bootstrap";
import Sketchpad from "./Sketchpad";
import "../Styles/Layout.css";

function Layout() {
  const [activeKey, setActiveKey] = useState("tab1");

  // 组件挂载时禁止滚动
  useEffect(() => {
    const originalStyle = window.getComputedStyle(document.body).overflow;
    document.body.style.overflow = "hidden";

    // 组件卸载时恢复滚动
    return () => (document.body.style.overflow = originalStyle);
  }, []);

  return (
    <Container fluid className="layout-container">
      <Row className="layout-head">
        <h1>AECademy Hub</h1>
      </Row>
      <Tabs
        id="justify-tab-example"
        activeKey={activeKey}
        onSelect={(k) => setActiveKey(k)}
        className="mb-3"
        justify
      >
        <Tab eventKey="tab1" title="Sketch">
          <Row className="layout-sketchpad">
            <Sketchpad />
          </Row>
          <Container className="layout-Btn-container">
            <Row className="layout-Btn-row">
              <Col xs={4} className="submit-Btn">
                <button className="btn btn-primary w-100">Submit</button>
              </Col>
              <Col xs={4} className="redo-Btn">
                <button className="btn btn-secondary w-100">Redo</button>
              </Col>
              <Col xs={4} className="undo-Btn">
                <button className="btn btn-success w-100">Undo</button>
              </Col>
            </Row>
          </Container>
        </Tab>
        <Tab eventKey="tab2" title="Selection">
          <h2>This is the content for Tab 2</h2>
          {/* Content for Tab 2 goes here */}
        </Tab>
      </Tabs>
    </Container>
  );
}

export default Layout;
