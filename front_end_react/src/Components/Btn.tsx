import React from "react";
import { Button } from "react-bootstrap";

interface CreateButtonProps {
  text?: string;
  onClick?: () => void;
  variant?: string;
  size?: "sm" | "lg";
  className?: string;
  type?: "button" | "submit" | "reset";
  disabled?: boolean;
}

const Btn: React.FC<CreateButtonProps> = ({
  text,
  onClick,
  variant,
  size = "sm",
  className,
  type = "button",
  disabled = false,
}) => {
  return (
    <Button
      variant={variant}
      size={size}
      className={className}
      onClick={onClick}
      type={type}
      disabled={disabled}
    >
      {text}
    </Button>
  );
};

export default Btn;
