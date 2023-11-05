import React from 'react';

const SubmitButton = ({ onImageSubmit }) => {
  return (
    <button className="btn btn-primary w-100" onClick={onImageSubmit}>
      Submit
    </button>
  );
};

export default SubmitButton;
