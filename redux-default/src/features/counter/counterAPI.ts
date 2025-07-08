export const fetchCount = (amount = 1): Promise<{ data: number }> =>
  new Promise<{ data: number }>(resolve =>
    setTimeout(() => {
      resolve({ data: amount })
    }, 500),
  )
