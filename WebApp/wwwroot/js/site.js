function LoadAnswers(listQuestion, listAnswerId) {
    var listAnswers = $("#" + listAnswerId);
    listAnswers.empty();

    var selectedQuestion = listQuestion.options[listQuestion.selectedIndex].value;

    if (selectedQuestion != null && selectedQuestion != '') {
        $.getJSON("/customer/quiz/GetAnswersByQuestion", { questionId: selectedQuestion }, function (answers) {
            if (answers != null && !jQuery.isEmptyObject(answers)) {
                $.each(answers, function (index, answer) {
                    listAnswers.append($('<option/>',
                        {
                            value: answer.value,
                            text: answer.text
                        }));
                });
                
            };
        });
    }

    return;
}