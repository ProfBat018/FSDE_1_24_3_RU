import { createContext, useState } from 'react'
import './App.css'
import { Counter } from './components/Counter'
import { Login } from './components/Login'
import { LoginWithContext } from './components/LoginWithContext'

export const ThemeContext = createContext();

function App() {
  const [theme, setTheme] = useState('light');

  return (
    // <>
    //   <form>
    //     <select onChange={(e) => setTheme(e.target.value)}>
    //       <option value="light">Light</option>
    //       <option value="dark">Dark</option>
    //     </select>
    //   </form>
    //   <Login theme={theme}/>
    // </>

    <ThemeContext.Provider value={theme}>
      <form>
        <select onChange={(e) => setTheme(e.target.value)}>
          <option value="light">Light</option>
          <option value="dark">Dark</option>
        </select>
      </form>
      <LoginWithContext />
    </ThemeContext.Provider>
  )
}

export default App;
