function isString(value: unknown): value is string {
  return (typeof value) === 'string';
}

function example(x: unknown) {
  if (isString(x)) {
    console.log(typeof x); // 'string'
  } else {
    console.log(typeof x); // 'unknown'
  }
}

example('Hello, world!'); // 'string'
example(42); // 'number'