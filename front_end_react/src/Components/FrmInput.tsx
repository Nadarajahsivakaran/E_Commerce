import React from "react";
import { Form } from "react-bootstrap";

interface TextInputProps {
  label: string;
  placeholder?: string;
  value: string;
  name: string;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  className?: string;
  type?: string;
  required?: boolean;
}

const FrmInput: React.FC<TextInputProps> = ({
  label,
  placeholder = "",
  value,
  name,
  onChange,
  className = "mb-3",
  type = "text",
  required = true,
}) => {
  return (
    <Form.Group className={className}>
      <Form.Label>{label}</Form.Label>
      <Form.Control
        type={type}
        placeholder={placeholder}
        value={value}
        name={name}
        onChange={onChange}
        required={required}
      />
      <Form.Control.Feedback type="invalid">
        {label} is required.
      </Form.Control.Feedback>
    </Form.Group>
  );
};

export default FrmInput;
