let connection = new signalR.HubConnectionBuilder().withUrl("/comments").build();

const compID = document.getElementById('comp-id-holder').innerHTML;
let commentsSection = document.getElementById('comments-section');

let textareaCommentText = document.getElementById('textarea-comment-text');


function generateComment(data) {
    let card = document.createElement('div');
    card.classList.add('card', 'mb-3');

    let cardbody = document.createElement('div');
    cardbody.classList.add('card-body');

    let cardtitle = document.createElement('h5');
    cardtitle.classList.add('card-title');
    cardtitle.id = data.ID;
    connection.invoke('GetUserName', data.AuthorID)
        .then((name) => {
            document.getElementById(data.ID).innerHTML = name;
        });
    cardbody.appendChild(cardtitle);

    let cardtext = document.createElement('p');
    cardtext.classList.add('card-text');
    cardtext.innerHTML = data["Contents"];
    cardbody.appendChild(cardtext);

    card.appendChild(cardbody);
    return card;
}

connection.start().then(resp => {
    connection.invoke('GetComments', compID)
        .then(comments => {
            if (comments == 'undefined' || comments == null) {
                return;
            }

            comments = JSON.parse(comments);

            comments.forEach(comment => {
                commentsSection.appendChild(generateComment(comment));
            });
        });
})
    .catch(err => console.log(err));

connection.on('AddComment', (comment) => {
    if (comment == 'undefined' || comment == null)
        return;

    comment = JSON.parse(comment);

    commentsSection.appendChild(generateComment(comment));
});

document.getElementById('btn-post-comment').addEventListener('click', () => {
    const commentText = textareaCommentText.value;

    connection.invoke('CreateComment', compID, commentText)
        .then(() => {
            textareaCommentText.value = "";
        })
        .catch(err => console.log(err))
});