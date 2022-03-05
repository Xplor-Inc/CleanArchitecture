import { createSlice } from '@reduxjs/toolkit'
import { UserState } from './hooks'

var initialState = {
    isLoggedIn: false,
    name: 'UnAuth',
    image: '/dynamic/images/no-image.jpg'
} as UserState

var p = localStorage.getItem('profile'); // get profile data if exists
if (p) {
    var pObj = JSON.parse(p) as UserState;
    initialState = pObj;
}

export const userProfileSlice = createSlice({
    name: 'profile',
    initialState: initialState,
    reducers: {
        updateProfileState: (state, action) => {
            updateStorage(action.payload)
            state.name = action.payload.name
            state.image = action.payload.image
            state.isLoggedIn = action.payload.isLoggedIn ?? state.isLoggedIn
        }
    }
})

const updateStorage = (data: UserState) => {
    var profile = JSON.stringify(data);
    localStorage.setItem('profile', profile);
}

export const { updateProfileState } = userProfileSlice.actions

export default userProfileSlice.reducer