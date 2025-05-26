import axios from "axios";
import { CategoryType } from "../../Types/categoryTypes";

// Optional reusable API response wrapper
type ApiResponse<T> = {
  res: T;
};

// Payload types
type CreateCategoryPayload = Omit<CategoryType, "categoryId">;
type UpdateCategoryPayload = CategoryType;

const CATEGORY_ENDPOINT = "/Category";

// Get all categories
const getCategories = async (): Promise<CategoryType[]> => {
  const response = await axios.get<ApiResponse<CategoryType[]>>(
    CATEGORY_ENDPOINT
  );
  return response.data.res;
};

// Create category
const createCategory = async (
  data: CreateCategoryPayload
): Promise<CategoryType> => {
  const response = await axios.post<ApiResponse<CategoryType>>(
    CATEGORY_ENDPOINT,
    data
  );
  return response.data.res;
};

// Update category
const updateCategory = async (
  data: UpdateCategoryPayload
): Promise<CategoryType> => {
  const response = await axios.put<ApiResponse<CategoryType>>(
    CATEGORY_ENDPOINT,
    data
  );
  return response.data.res;
};

const deleteCategory = async (id: number): Promise<number> => {
  const response = await axios.delete<ApiResponse<number>>(
    `${CATEGORY_ENDPOINT}/${id}`
  );
  return response.data.res;
};

// Export service object
export const CategoryService = {
  getCategories,
  createCategory,
  updateCategory,
  deleteCategory,
};
