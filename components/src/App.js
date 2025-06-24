import './App.css';
import Counter from './appcomponents/Counter';
import LifecycleDemo from './appcomponents/LifecycleDemo';
import React, { useState } from 'react';

function App() {
  const [a, setA] = useState(true);

  setTimeout(() => {
    setA(false);
  }, 1000);

  return (
    <div className="App">
      <h1>Welcome to the Counter App</h1>
      {a && <LifecycleDemo />}
      <p>This is a simple demonstration of React component lifecycle methods.</p>
    </div>
  );
}

export default App;
