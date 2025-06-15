
export default function Button(props) {
  return (
    <button className="btn">
      {props.name || 'Click Me'}
    </button>
  );
}



