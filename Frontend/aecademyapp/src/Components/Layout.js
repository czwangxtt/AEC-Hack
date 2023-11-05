import React, { useState, useEffect } from "react";
import { Container, Row, Col, Tabs, Tab } from "react-bootstrap";
import Sketchpad from "./Sketchpad";
import "../Styles/Layout.css";
import SubmitButton from "./SubmitButton";
import { useData } from "./DataContext";

function Layout() {
  const [activeKey, setActiveKey] = useState("tab1");
  const [canvas, setCanvas] = useState(null); // 定义状态来存储canvas引用
  const [imagePreviewUrl, setImagePreviewUrl] = useState(""); // 新的状态变量保存图片的URL
  const { setData } = useData(); // 使用useData钩子

  // Disable scrolling when component is mounted
  useEffect(() => {
    const originalStyle = window.getComputedStyle(document.body).overflow;
    document.body.style.overflow = "hidden";

    // Resume scrolling when component is uninstalled
    return () => (document.body.style.overflow = originalStyle);
  }, []);

  const handleCanvasReady = (canvasElement) => {
    setCanvas(canvasElement);
  };

  const getBase64Image = () => {
    if (canvas) {
      return canvas.toDataURL("image/png");
    }
    return null;
  };

  const handleSubmit = async () => {
    const base64ImageData = getBase64Image();
    // console.log("Submit button clicked!", base64ImageData);
    const prompt = "test";

    if (base64ImageData) {
      try {
        // use agnet proxy
        const response = await fetch("/api/Suggestion/GetSuggestion", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({
            prompt: prompt,
            base64ImageData: base64ImageData,
          }),
        });

        if (response.ok) {
          const responseData = await response.json();
          setImagePreviewUrl(responseData.data[1].previewUrl);
          console.log(responseData.data[1].previewUrl);
          setData(responseData); // 存储整个响应数据，以便在其他组件中使用
          console.log(responseData); // 这里可以根据响应做进一步处理
        } else {
          throw new Error("Something went wrong on api server!");
        }
      } catch (error) {
        console.error(
          "There has been a problem with your fetch operation:",
          error
        );
      }
    } else {
      console.error("Canvas is not ready or image data is not available");
    }
  };

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
            <Sketchpad onSave={handleCanvasReady} />
          </Row>
          <Container className="layout-Btn-container">
            <Row className="layout-Btn-row">
              <Col xs={4} className="submit-Btn">
                <SubmitButton onImageSubmit={handleSubmit} />
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
          {imagePreviewUrl && <img src={imagePreviewUrl} alt="Preview" />}{" "}
        </Tab>
      </Tabs>
    </Container>
  );
}

export default Layout;
