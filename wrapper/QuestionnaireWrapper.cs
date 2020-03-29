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
            // create store model questionnaire
            Questionnaire questionnaire = new Questionnaire();
            questionnaire.Category = _questionnaireViewModel.Category;
            questionnaire.Version = _questionnaireViewModel.Version;
            questionnaire.Questions = new Collection<Question>();
            questionnaire.QuestionRules = new Collection<QuestionRule>();

            // create stopre model questions and questionrules
            foreach(QuestionFormly viewModelQuestion in _questionnaireViewModel.Schema){
                Question question = new Question();
                question.QuestionTemplate = new Collection<Template>();
                QuestionRule questionRule = new QuestionRule();

                question.Name = viewModelQuestion.Key;
                question.Type = viewModelQuestion.Type;
                question.Version = viewModelQuestion.Version;

                questionRule.Name = viewModelQuestion.Key;
                questionRule.HideQuestion = viewModelQuestion.HideExpression;
                
                if(viewModelQuestion.ExpressionProperties != null){
                    foreach(var y in viewModelQuestion.ExpressionProperties){
                        ExpressionModel epressionModel = new ExpressionModel();
                        epressionModel.Key = y.Key;
                        epressionModel.Expression = y.Value;
                        questionRule.ExpressionModel.Add(epressionModel);
                    }
                }

                foreach(TemplateFormly template in viewModelQuestion.TemplateOptions){
                        // check if question is required
                        questionRule.Required = template.Required;

                        // Add all templateswith different languages to the template
                        Template questionTemplate = new Template();
                        questionTemplate.Description = template.Description;
                        questionTemplate.InputType = template.Type;
                        questionTemplate.Label = template.Label;
                        questionTemplate.Language = template.Language;
                        questionTemplate.Placeholder = template.Placeholder;
                        questionTemplate.Version = template.Version;
                        
                        if(template.Options != null){
                            questionTemplate.SelectAllOption = template.selectAllOption;
                            questionTemplate.MultipleChoiceQuestion = template.multiple;
                            questionTemplate.Options = new Collection<MultipleChoiceOption>();

                            foreach(FormlyOption option in template.Options){
                                MultipleChoiceOption multipleChoiceOption = new MultipleChoiceOption();
                                questionTemplate.Options.Add(multipleChoiceOption);
                            }
                            question.QuestionTemplate.Add(questionTemplate);
                        }
                        else{
                            question.QuestionTemplate.Add(questionTemplate);
                        }
                    }
                    questionnaire.Questions.Add(question);
                    questionnaire.QuestionRules.Add(questionRule);
                }

            return questionnaire;
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
