using System.Collections.Generic;

namespace questionnaireBackend.wrapper
{
    public interface IQuestionnaireWrapper
    {
        void SubstituteAnsweredQuestion(int questionId, string answer);
        List<int> GetAllQuestionsId();
        Questionnaire GetStoreModel();
        QuestionnaireViewModel GetViewModel();

        List<AssetQuestionModel> GetAllAnsweredQuestions();
        List<AssetQuestionModel> GetAllUnAnsweredQuestions();

    }
}
