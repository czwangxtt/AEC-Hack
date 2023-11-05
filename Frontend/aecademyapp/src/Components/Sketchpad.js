import React, {
  useRef,
  useEffect,
  useState,
  useImperativeHandle,
  forwardRef,
} from 'react';

const Sketchpad = forwardRef((props, ref) => {
  const canvasRef = useRef(null);
  const contextRef = useRef(null);
  const [isDrawing, setIsDrawing] = useState(false);
  const [history, setHistory] = useState([]);
  const [redoList, setRedoList] = useState([]);

  useImperativeHandle(ref, () => ({
    undo,
    redo,
    clear,
  }));

  useEffect(() => {
    const canvas = canvasRef.current;
    canvas.width = window.innerWidth * 2;
    canvas.height = window.innerHeight * 2;
    canvas.style.width = `${window.innerWidth}px`;
    canvas.style.height = `${window.innerHeight}px`;

    const context = canvas.getContext('2d');
    context.scale(2, 2);
    context.lineCap = 'round';
    context.strokeStyle = 'black';
    context.lineWidth = 5;
    contextRef.current = context;
  }, []);

  const startDrawing = ({ nativeEvent }) => {
    const { offsetX, offsetY } = nativeEvent;
    contextRef.current.beginPath();
    contextRef.current.moveTo(offsetX, offsetY);
    setIsDrawing(true);

    const dataUrl = canvasRef.current.toDataURL();
    setHistory([...history, dataUrl]);
    setRedoList([]);
  };

  const finishDrawing = () => {
    contextRef.current.closePath();
    setIsDrawing(false);
  };

  const draw = ({ nativeEvent }) => {
    if (!isDrawing) {
      return;
    }
    const { offsetX, offsetY } = nativeEvent;
    contextRef.current.lineTo(offsetX, offsetY);
    contextRef.current.stroke();
  };

  const undo = () => {
    if (history.length === 0) return;
    setRedoList([...redoList, history[history.length - 1]]);
    setHistory(history.slice(0, -1));
    restoreCanvas(history[history.length - 2]);
  };

  const redo = () => {
    if (redoList.length === 0) return;
    setHistory([...history, redoList[redoList.length - 1]]);
    setRedoList(redoList.slice(0, -1));
    restoreCanvas(redoList[redoList.length - 1]);
  };

  const clear = () => {
    const context = contextRef.current;
    context.clearRect(0, 0, canvasRef.current.width, canvasRef.current.height);
    setHistory([]);
    setRedoList([]);
  };

  const restoreCanvas = (imageUrl) => {
    const image = new Image();
    image.src = imageUrl;
    image.onload = () => {
      contextRef.current.clearRect(0, 0, canvasRef.current.width, canvasRef.current.height);
      contextRef.current.drawImage(image, 0, 0, canvasRef.current.width, canvasRef.current.height);
    };
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
});

export default Sketchpad;
