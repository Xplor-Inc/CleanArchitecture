import { createSlice } from '@reduxjs/toolkit'
import { UserState } from './hooks'

const initialState = {
    isLoggedIn: false,
    name: 'UnAuth',
    image: '/dynamic/images/no-image.jpg'
} as UserState

export const userProfileSlice = createSlice({
    name: 'profile',
    initialState: initialState,
    reducers: {
        updateProfileState: (state, action) => {
            state.name = action.payload.name
            state.image = action.payload.image
            state.isLoggedIn = action.payload.isLoggedIn ?? state.isLoggedIn
        }
    }
})

export const { updateProfileState } = userProfileSlice.actions

export default userProfileSlice.reducer