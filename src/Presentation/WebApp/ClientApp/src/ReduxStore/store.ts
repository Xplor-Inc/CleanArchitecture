import { configureStore } from '@reduxjs/toolkit'
import counterReducer from './userProfileSlice'

const store = configureStore({
  reducer: {
    user: counterReducer
  }
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch
export default store;