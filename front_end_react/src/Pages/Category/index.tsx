import { useEffect, useState } from "react";
import { useAppDispatch, useAppSelector } from "../../hooks";
import {
  addCategories,
  dropCategory,
  fetchCategories,
  updateCategory,
} from "../../Features/Category/CategorySlice";
import Btn from "../../Components/Btn";
import Create from "./Create";
import { showError, showSuccess } from "../../Utils/toast";
import Loader from "../../Components/Loader";
import Message from "../../Components/Message";
import CategoryTable from "./CategoryTable";
import { confirmDelete } from "../../Utils/confirmDelete";
import type { CategoryType  } from "../../Types/categoryTypes";

const Category = () => {
  const dispatch = useAppDispatch();

  const { categories, loading, error } = useAppSelector(
    (state) => state.category
  );

  useEffect(() => {
    dispatch(fetchCategories());
  }, [dispatch]);

  const initialFormData: CategoryType  = {
    categoryId: 0,
    categoryName: "",
    categoryDescription: "",
  };

  const [show, setShow] = useState(false);
  const [saveDisable, setSaveDisable] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => {
    setFormData(initialFormData);
    setBtnText("Create");
    setShow(true);
  };

  const [formData, setFormData] = useState<CategoryType >(initialFormData);
  const [btnText, setBtnText] = useState("Create");

  const onChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData((prevState) => ({
      ...prevState,
      [e.target.name]: e.target.value,
    }));
  };

  const handleSave = async () => {
    if (formData.categoryName.trim() === "") {
      showError("Category Name is required.");
      return;
    }

    setSaveDisable(true);

    try {
      if (formData.categoryId === 0) {
        await dispatch(
          addCategories({
            categoryName: formData.categoryName,
            categoryDescription: formData.categoryDescription,
          })
        ).unwrap();
        showSuccess("Category Created Successfully!");
      } else {
        await dispatch(updateCategory(formData)).unwrap();
        showSuccess("Category Updated Successfully!");
      }
      handleClose();
    } catch (err) {
      showError(err);
    } finally {
      setSaveDisable(false);
    }
  };

  const handleEdit = (category: typeof formData) => {
    setFormData(category);
    setBtnText("Update");
    setShow(true);
  };

  const handleDelete = async (id: number) => {
    const isConfirmed = await confirmDelete();
    if (isConfirmed) {
      try {
        dispatch(dropCategory(id))
          .unwrap()
          .then(() => showSuccess("Category Deleted Successfully!"))
          .catch((err) => showError(err));
      } catch (err) {
        showError(err);
      }
    }
  };

  if (loading) return <Loader />;
  if (error) return <Message variant="danger">{error}</Message>;

  return (
    <>
      <div className="d-flex justify-content-between align-items-center my-4">
        <h1 className="text-center flex-grow-1 m-0">Categories</h1>
        <Btn text="CREATE" variant="success" onClick={handleShow} />
      </div>
      {categories.length === 0 ? (
        <Message variant="info">No categories available.</Message>
      ) : (
        <CategoryTable
          categories={categories}
          onEdit={handleEdit}
          onDelete={handleDelete}
        />
      )}
      <Create
        show={show}
        onHide={handleClose}
        onSave={handleSave}
        formData={formData}
        onChange={onChange}
        saveButtonText={btnText}
        disableSaveBtn={saveDisable}
      />
    </>
  );
};

export default Category;
