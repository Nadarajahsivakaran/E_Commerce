import React from "react";
import { Table } from "react-bootstrap";
import Btn from "../../Components/Btn";

type Category = {
  categoryId: number;
  categoryName: string;
  categoryDescription: string;
};

interface Props {
  categories: Category[];
  onEdit: (category: Category) => void;
  onDelete?: (id: number) => void;
}

const CategoryTable: React.FC<Props> = ({ categories, onEdit, onDelete }) => {
  return (
    <Table striped bordered hover responsive>
      <thead>
        <tr>
          <th>#</th>
          <th>Category</th>
          <th>Description</th>
          <th>Action</th>
        </tr>
      </thead>
      <tbody>
        {categories.map((category, index) => (
          <tr key={category.categoryId}>
            <td>{index + 1}</td>
            <td>{category.categoryName}</td>
            <td>{category.categoryDescription}</td>
            <td>
              <Btn
                text="Edit"
                variant="primary"
                className="me-2"
                onClick={() => onEdit(category)}
              />
              {onDelete && (
                <Btn
                  text="Delete"
                  variant="danger"
                  onClick={() => onDelete(category.categoryId)}
                />
              )}
            </td>
          </tr>
        ))}
      </tbody>
    </Table>
  );
};

export default CategoryTable;
