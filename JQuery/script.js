const form = $(`#form`);
const paragraph = $(`#message`);

form.on(`submit`, (e) => {
    e.preventDefault();
    const data = {
        name: $(`#name`).val()
    };
  
    paragraph.text(`Form submitted with data: ${JSON.stringify(data)}`);
});


