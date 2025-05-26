import Btn from "./Btn";
import { Form, Modal } from "react-bootstrap";
import React, { useState, useEffect } from "react";

interface ModalWrapperProps {
  show: boolean;
  onHide: () => void;
  onSave: () => void;
  title: string;
  children: React.ReactNode;
  saveButtonText?: string;
  disableFooter?: boolean;
  disableSaveBtn?: boolean;
}

const FrmModal: React.FC<ModalWrapperProps> = ({
  show,
  onHide,
  onSave,
  title,
  children,
  saveButtonText = "Save Changes",
  disableFooter = false,
  disableSaveBtn = false,
}) => {
  const [validated, setValidated] = useState(false);

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    e.stopPropagation();

    const form = e.currentTarget;
    if (form.checkValidity()) onSave();
    setValidated(true);
  };

  useEffect(() => {
    if (!show) setValidated(false);
  }, [show]);

  return (
    <Modal show={show} onHide={onHide}>
      <Modal.Header closeButton>
        <Modal.Title>{title}</Modal.Title>
      </Modal.Header>
      <Form noValidate validated={validated} onSubmit={handleSubmit}>
        {children}
        {!disableFooter && (
          <Modal.Footer>
            <Btn variant="secondary" onClick={onHide} text="Close" />
            <Btn
              variant="primary"
              type="submit"
              text={saveButtonText}
              disabled={disableSaveBtn}
            />
          </Modal.Footer>
        )}
      </Form>
    </Modal>
  );
};

export default FrmModal;
