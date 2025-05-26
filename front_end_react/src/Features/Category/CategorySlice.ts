import { createAsyncThunk, createSlice,PayloadAction  } from "@reduxjs/toolkit";
import { CategoryService } from "./categoryService";
import { CategoryType  } from "../../Types/categoryTypes";



interface CategoryState {
  categories: CategoryType [];
  loading: boolean;
  error: string | null;
}

const initialState: CategoryState = {
  categories: [],
  loading: false,
  error: null,
};

// Thunks
export const fetchCategories = createAsyncThunk<CategoryType[]>(
  "category/fetchCategories",
  async (_, thunkAPI) => {
    try {
      return await CategoryService.getCategories();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const addCategories = createAsyncThunk<
  CategoryType,
  Omit<CategoryType, "categoryId">
>("category/addCategories", async (data, thunkAPI) => {
  try {
    return await CategoryService.createCategory(data);
  } catch (error: any) {
    return thunkAPI.rejectWithValue(error.message);
  }
});


export const updateCategory = createAsyncThunk<CategoryType, CategoryType>(
  "category/updateCategory",
  async (data, thunkAPI) => {
    try {
      return await CategoryService.updateCategory(data);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const dropCategory = createAsyncThunk<number, number>(
  "category/dropCategory",
  async (id, thunkAPI) => {
    try {
      await CategoryService.deleteCategory(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);


// Slice
const CategorySlice = createSlice({
  name: "category",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
    // Fetch
      .addCase(fetchCategories.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCategories.fulfilled, (state, action: PayloadAction<CategoryType[]>) => {
        state.loading = false;
        state.categories = action.payload;
      })
      .addCase(fetchCategories.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Add
      .addCase(addCategories.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(addCategories.fulfilled, (state, action: PayloadAction<CategoryType>) => {
        state.loading = false;
        state.categories.push(action.payload);
      })
      .addCase(addCategories.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Update
      .addCase(updateCategory.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
        .addCase(updateCategory.fulfilled, (state, action: PayloadAction<CategoryType>) => {
        state.loading = false;
        const updatedCategory = action.payload;
        const index = state.categories.findIndex(
          (category) => category.categoryId === updatedCategory.categoryId
        );
        if (index !== -1) state.categories[index] = updatedCategory;
      })
      .addCase(updateCategory.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Delete
      .addCase(dropCategory.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(dropCategory.fulfilled, (state, action) => {
        state.loading = false;
        state.categories = state.categories.filter(
          (category) => category.categoryId !== action.payload
        );
      })
      .addCase(dropCategory.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      });
  },
});

export default CategorySlice.reducer;
