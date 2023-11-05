import React, { useState, useEffect, useRef } from "react";
import { Container, Row, Col, Tabs, Tab } from "react-bootstrap";
import Sketchpad from "./Sketchpad";
import "../Styles/Layout.css";
import SubmitButton from "./SubmitButton";
import { useData } from "./DataContext";

function Layout() {
  const [activeKey, setActiveKey] = useState("tab1");
  const [canvas, setCanvas] = useState(null); // 定义状态来存储canvas引用
  const { setData } = useData(); // 使用useData钩子
  const apiUrl = process.env.REACT_APP_API_URL || "/api"; // 本地开发时回退到代理
  const [dataItems, setDataItems] = useState([]); // 存储整个数据的数组
  const [selectedItems, setSelectedItems] = useState({}); //义一个状态来跟踪选中的项目
  const sketchpadRef = useRef();

  // Disable scrolling when component is mounted
  useEffect(() => {
    const originalStyle = window.getComputedStyle(document.body).overflow;
    document.body.style.overflow = "hidden";

    // Resume scrolling when component is uninstalled
    return () => (document.body.style.overflow = originalStyle);
  }, []);

  const handleClear = () => {
    sketchpadRef.current.clearCanvas(); // 通过ref调用Sketchpad的clearCanvas方法
  };
  const handleCanvasReady = (canvasElement) => {
    setCanvas(canvasElement);
  };

  const getBase64Image = () => {
    if (canvas && canvas.toDataURL) {
      // 确保canvas是一个canvas DOM元素
      return canvas.toDataURL("image/png");
    }
    return null;
  };
  const handleCheckboxChange = (e, guid, item) => {
    const checked = e.target.checked;
    setSelectedItems((prev) => {
      // 如果选中了，添加到selectedItems中
      if (checked) {
        return { ...prev, [guid]: item };
      } else {
        // 如果取消了选中，从selectedItems中移除
        const updated = { ...prev };
        delete updated[guid];
        return updated;
      }
    });
  };

  const sendPutRequest = async () => {
    const entries = Object.values(selectedItems);
    for (const item of entries) {
      const requestBody = {
        userGuid: item.author, // 根据实际情况获取 userGuid
        objectGuid: item.guid,
      };

      try {
        const response = await fetch("/api/Queue", {
          method: "PUT",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(requestBody),
        });

        // 检查响应是否成功
        if (!response.ok) {
          throw new Error("Failed to PUT data");
        }

        const responseData = await response.json();
        console.log("Success:", responseData);
      } catch (error) {
        console.error("Error:", error);
      }
    }
  };

  const handleSubmit = async () => {
    const base64ImageData = getBase64Image();
    // console.log("Submit button clicked!", base64ImageData);
    const prompt = "test";

    if (base64ImageData) {
      console.log(`${apiUrl}/Suggestion/GetSuggestion`);
      try {
        // use agnet proxy
        const response = await fetch(`${apiUrl}/Suggestion/GetSuggestion`, {
          // const response = await fetch("/api/Suggestion/GetSuggestion", {
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
          // console.log(responseData.data[1].previewUrl);
          //10 image testing
          setDataItems(responseData.data.slice(0, 10)); // 取前10个数据项
          const urls = responseData.data.map((item) => item.previewUrl); // 假设每个数据项都有 previewUrl
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
            <Sketchpad ref={sketchpadRef} onSave={handleCanvasReady} />
          </Row>
          <Container className="layout-Btn-container">
            <Row className="layout-Btn-row">
              <Col xs={6} className="submit-Btn">
                <SubmitButton onImageSubmit={handleSubmit} />
              </Col>
              {/* <Col xs={3} className="redo-Btn">
                <button className="btn btn-secondary w-100">Redo</button>
              </Col>
              <Col xs={3} className="undo-Btn">
                <button className="btn btn-success w-100">Undo</button>
              </Col> */}
              <Col xs={6} className="clear-Btn">
                <button
                  className="btn btn-secondary w-100"
                  onClick={handleClear}
                >
                  Clear
                </button>
              </Col>
            </Row>
          </Container>
        </Tab>
        <Tab eventKey="tab2" title="Selection">
          <h2>Select your preference!</h2>
          <div className="image-gallery">
            {dataItems.map((item, index) => (
              <div
                key={item.guid}
                className={`image-container ${
                  index % 2 === 0 ? "new-row" : ""
                }`}
              >
                <input
                  type="checkbox"
                  checked={!!selectedItems[item.guid]}
                  onChange={(e) => handleCheckboxChange(e, item.guid, item)}
                />
                {item.previewUrl && <img src={item.previewUrl} alt="Preview" />}
                <p className="description">{item.description}</p>
              </div>
            ))}
          </div>
          <button className="btn btn-primary w-100" onClick={sendPutRequest}>
            Submit Selected
          </button>
        </Tab>
      </Tabs>
    </Container>
  );
}

export default Layout;
