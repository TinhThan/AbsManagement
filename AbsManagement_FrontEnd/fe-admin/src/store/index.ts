import { combineReducers, configureStore } from '@reduxjs/toolkit';
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux';
import { persistReducer, persistStore } from 'redux-persist';
import storage from 'redux-persist/lib/storage';

import authReducer from './auth.store'

const persistConfig = {
    key: 'root',
    storage
}

const rootReducer = combineReducers({
    auth: authReducer,
})

const persistedReducer = persistReducer(persistConfig, rootReducer);

const store = configureStore({
    reducer: persistedReducer,
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware({
            immutableCheck: false,
            serializableCheck: false,
        }),
})

export default store

export type IStore = typeof store
export type RootState = ReturnType<typeof rootReducer>
export type AppDispatch = typeof store.dispatch
export type GetState = () => RootState
export const persistor = persistStore(store)

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector