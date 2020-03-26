using System.Collections.Generic;

namespace questionnaireBackend
{
    public interface IQuestionnaireWrapper
    {
        void SubstituteAnsweredQuestion(int QuestionId, string Answer);
        List<int> GetAllQuestionsID();
        Questionnaire GetStoreModel();
        QuestionnaireViewModel GetViewModel();

        List<AssetQuestionModel> GetAllAnsweredQuestions();
        List<AssetQuestionModel> GetAllUnAnsweredQuestions();

    }
}
