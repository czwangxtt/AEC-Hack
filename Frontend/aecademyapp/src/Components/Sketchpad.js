import React, { useRef, useEffect, useState } from "react";

const Sketchpad = ({ onSave }) => {
  const canvasRef = useRef(null);
  const contextRef = useRef(null);
  const [isDrawing, setIsDrawing] = useState(false);
  // 栈来保存画图历史记录，以支持撤销操作
  const [history, setHistory] = useState([]);
  // 栈来保存撤销的状态，以支持重做操作
  const [redoList, setRedoList] = useState([]);
  const exportImage = () => {
    const base64ImageData = canvasRef.current.toDataURL("image/png");
    onSave(base64ImageData);
  };

  useEffect(() => {
    const canvas = canvasRef.current;
    canvas.width = window.innerWidth * 2;
    canvas.height = window.innerHeight * 2;
    canvas.style.width = `${window.innerWidth}px`;
    canvas.style.height = `${window.innerHeight}px`;

    const context = canvas.getContext("2d");
    context.scale(2, 2);
    context.lineCap = "round";
    context.strokeStyle = "black";
    context.lineWidth = 5;
    contextRef.current = context;
  }, []);

  useEffect(() => {
    onSave(canvasRef.current);
  }, [onSave]);

  const startDrawing = (event) => {
    const { offsetX, offsetY } = getCoordinates(event);
    contextRef.current.beginPath();
    contextRef.current.moveTo(offsetX, offsetY);
    setIsDrawing(true);

    // 在开始绘制之前，保存当前状态
    const canvas = canvasRef.current;
    const dataUrl = canvas.toDataURL();
    setHistory([...history, dataUrl]);
    setRedoList([]); // 开始新的绘画会清空可重做的历史
  };

  const finishDrawing = () => {
    contextRef.current.closePath();
    setIsDrawing(false);
  };

  const draw = (event) => {
    if (!isDrawing) {
      return;
    }
    const { offsetX, offsetY } = getCoordinates(event);
    contextRef.current.lineTo(offsetX, offsetY);
    contextRef.current.stroke();
  };

  // Helper function to get the coordinates based on event type
  const getCoordinates = (event) => {
    if (event.touches && event.touches.length > 0) {
      // For touch events
      const touch = event.touches[0];
      return {
        offsetX: touch.clientX - touch.target.offsetLeft,
        offsetY: touch.clientY - touch.target.offsetTop,
      };
    } else {
      // For mouse events
      return {
        offsetX: event.nativeEvent.offsetX,
        offsetY: event.nativeEvent.offsetY,
      };
    }
  };

  return (
    <canvas
      onMouseDown={startDrawing}
      onMouseUp={finishDrawing}
      onMouseMove={draw}
      onMouseOut={finishDrawing}
      onTouchStart={startDrawing}
      onTouchMove={draw}
      onTouchEnd={finishDrawing}
      ref={canvasRef}
    />
  );
};

export default Sketchpad;
