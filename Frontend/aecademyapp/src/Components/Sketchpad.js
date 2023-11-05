import React, {
  useRef,
  useEffect,
  useState,
  useImperativeHandle,
  forwardRef,
} from "react";

const Sketchpad = forwardRef(({ onSave }, ref) => {
  const canvasRef = useRef(null);
  const contextRef = useRef(null);

  const [isDrawing, setIsDrawing] = useState(false);

  useImperativeHandle(ref, () => ({
    clearCanvas,
  }));
  useEffect(() => {
    const canvas = canvasRef.current;
    const displayWidth = window.innerWidth; // 浏览器视口的宽度
    const displayHeight = window.innerHeight * 0.8; // 80% 的视口高度

    // 由于您想要在高分辨率屏幕（如Retina显示屏）上保持清晰度，实际画布的像素数是CSS像素数的两倍
    canvas.width = displayWidth * 2;
    canvas.height = displayHeight * 2;

    // CSS样式控制画布在浏览器中展示时的尺寸
    canvas.style.width = `${displayWidth}px`;
    canvas.style.height = `${displayHeight}px`;

    // 设置2D上下文的属性
    const context = canvas.getContext("2d");
    context.scale(2, 2); // 缩放上下文，以保持画布在高分辨率屏幕上的清晰度
    context.lineCap = "round";
    context.strokeStyle = "black";
    context.lineWidth = 5;
    contextRef.current = context;
  }, []);

  const startDrawing = (event) => {
    const { offsetX, offsetY } = getCoordinates(event);
    contextRef.current.beginPath();
    contextRef.current.moveTo(offsetX, offsetY);
    setIsDrawing(true);
  };
  const finishDrawing = () => {
    contextRef.current.closePath();
    setIsDrawing(false);
    if (onSave && canvasRef.current instanceof HTMLCanvasElement) {
      // 确保是canvas元素
      onSave(canvasRef.current); // 传递canvas DOM元素
    }
  };

  const draw = (event) => {
    if (!isDrawing) {
      return;
    }
    const { offsetX, offsetY } = getCoordinates(event);
    contextRef.current.lineTo(offsetX, offsetY);
    contextRef.current.stroke();
  };

  const clearCanvas = () => {
    const context = contextRef.current;
    context.clearRect(0, 0, canvasRef.current.width, canvasRef.current.height);
  };

  const getCoordinates = (event) => {
    if (event.touches && event.touches.length > 0) {
      const touch = event.touches[0];
      return {
        offsetX: touch.clientX - touch.target.offsetLeft,
        offsetY: touch.clientY - touch.target.offsetTop,
      };
    } else {
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
      ref={canvasRef} // 确保这里的ref设置正确
    />
  );
});

export default Sketchpad;
