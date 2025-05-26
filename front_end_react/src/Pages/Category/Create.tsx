import React from "react";
import FrmInput from "../../Components/FrmInput";
import FrmModal from "../../Components/FrmModal";
import { Modal } from "react-bootstrap";

interface CreateCategoryModalProps {
  show: boolean;
  onHide: () => void;
  onSave: () => void;
  formData: {
    categoryName: string;
    categoryDescription: string;
  };
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
  saveButtonText?: string;
  disableSaveBtn?:boolean
}

const Create: React.FC<CreateCategoryModalProps> = ({
  show,
  onHide,
  onSave,
  formData,
  onChange,
  saveButtonText,
  disableSaveBtn
}) => {
  return (
    <FrmModal
      show={show}
      onHide={onHide}
      onSave={onSave}
      title="Create Category"
      saveButtonText={saveButtonText}
      disableSaveBtn={disableSaveBtn}
    >
      <Modal.Body>
        <FrmInput
          label="Category Name"
          placeholder="Enter Category Name"
          value={formData.categoryName}
          name="categoryName"
          onChange={onChange}
        />
        <FrmInput
          label="Category Description"
          placeholder="Enter Category Description"
          value={formData.categoryDescription}
          name="categoryDescription"
          onChange={onChange}
          required={false}
        />
      </Modal.Body>
    </FrmModal>
  );
};

export default Create;
