using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace questionnaireBackend
{
    public class QuestionnaireWrapper : IQuestionnaireWrapper
    {
        private Questionnaire _questionnaireStoreModel = new Questionnaire();
        private QuestionnaireViewModel _questionnaireViewModel = new QuestionnaireViewModel();

        public QuestionnaireWrapper(QuestionnaireViewModel questionnaireViewModel){
            this._questionnaireViewModel = questionnaireViewModel;
            this._questionnaireStoreModel = new QuestionnaireWrapper(questionnaireViewModel).GetStoreModel();
        }

        public QuestionnaireWrapper(Questionnaire questionnaireStoreModel){
            this._questionnaireStoreModel = questionnaireStoreModel;
        }

        public List<AssetQuestionModel> GetAllAnsweredQuestions()
        {
            throw new System.NotImplementedException();
        }

        public List<int> GetAllQuestionsID()
        {
            throw new System.NotImplementedException();
        }

        public List<AssetQuestionModel> GetAllUnAnsweredQuestions()
        {
            throw new System.NotImplementedException();
        }

        public Questionnaire GetStoreModel()
        {
            Questionnaire questionnaire = new Questionnaire();
            questionnaire.Category = _questionnaireViewModel.Category;
            questionnaire.Version = _questionnaireViewModel.Version;
            questionnaire.Questions = new Collection<Question>();
            questionnaire.QuestionRules = new Collection<QuestionRules>();

            foreach(ChildFormlySchema viewModelQuestion in _questionnaireViewModel.Schema){
                Question question = new Question();
                question.Name = viewModelQuestion.Key;
                question.Type = viewModelQuestion.Type;
                question.Version = viewModelQuestion.Version;

                foreach(TemplateOptionsFormlyModel template in viewModelQuestion.TemplateOptions){
                    if(template.multiple == true){

                        MultipleChoiceTemplate questionTemplate = new MultipleChoiceTemplate();

                        questionTemplate.Description = template.Description;
                        questionTemplate.InputType = template.Type;
                        questionTemplate.Label = template.Label;
                        questionTemplate.Language = template.Language;
                        questionTemplate.Placeholder = template.Placeholder;
                        questionTemplate.Version = template.Version;
                        questionTemplate.SelectAllOption = template.selectAllOption;
                        questionTemplate.Options = new Collection<MultipleChoiceOption>();

                        foreach(FormlyOption option in template.Options){
                            MultipleChoiceOption multipleChoiceOption = new MultipleChoiceOption();
                            questionTemplate.Options.Add(multipleChoiceOption);
                        }
                       // questionnaire.Questions.Add(questionTemplate);

                        

                    }
                    else{
                        Template questionTemplate = new Template();
                        questionTemplate.Description = template.Description;
                        questionTemplate.InputType = template.Type;
                        questionTemplate.Label = template.Label;
                        questionTemplate.Language = template.Language;
                        questionTemplate.Placeholder = template.Placeholder;
                        questionTemplate.Version = template.Version;
                    }
                    
                    

                }

                

                
            }
            throw new System.NotImplementedException();
        }

        public QuestionnaireViewModel GetViewModel()
        {
            throw new System.NotImplementedException();
        }

        public void SubstituteAnsweredQuestion(int QuestionId, string Answer)
        {
            throw new System.NotImplementedException();
        }
    }
}
