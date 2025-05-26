
import { configureStore } from '@reduxjs/toolkit';
import CategoryReducer from './Features/Category/CategorySlice'

export const store = configureStore({
  reducer: {
    category: CategoryReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;