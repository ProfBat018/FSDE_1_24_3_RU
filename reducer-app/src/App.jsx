import { useReducer } from 'react'
import './App.css'
import AuthForm from './AuthForm'

function App() {

  const reducer = (state, action) => {
    switch (action.type) {
      case 'SET_MODE':
        return { ...state, mode: action.payload };
      default:
        return state;
    }
  }

  const initialState = { mode: 'login' };

  const [state, dispatch] = useReducer(reducer, initialState);
  
  return (
    <>
    <AuthForm mode={state.mode} dispatch={dispatch}/>
    </>
  )
}

export default App
