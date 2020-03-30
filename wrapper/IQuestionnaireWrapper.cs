using System.Collections.Generic;
using questionnaireBackend.models;

namespace questionnaireBackend.wrapper
{
    public interface IQuestionnaireWrapper
    {
        void SubstituteAnsweredQuestion(int questionId, string answer);
        List<int> GetAllQuestionsId();
        Questionnaire GetStoreModel();
        QuestionnaireGetViewModel GetViewModel(string language);

        List<AssetQuestionModel> GetAllAnsweredQuestions();
        List<AssetQuestionModel> GetAllUnAnsweredQuestions();

    }
}
