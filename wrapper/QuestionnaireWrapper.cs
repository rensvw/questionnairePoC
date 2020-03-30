using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace questionnaireBackend.wrapper
{
    public class QuestionnaireWrapper : IQuestionnaireWrapper
    {
        private Questionnaire _questionnaireStoreModel = new Questionnaire();
        private readonly QuestionnaireViewModel _questionnaireViewModel = new QuestionnaireViewModel();

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

        public List<int> GetAllQuestionsId()
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
            var questionnaire = new Questionnaire
            {
                Category = _questionnaireViewModel.Category,
                Version = _questionnaireViewModel.Version,
                Questions = new Collection<Question>(),
                QuestionRules = new Collection<QuestionRule>()
            };

            // create stopre model questions and questionrules
            foreach(var viewModelQuestion in _questionnaireViewModel.Questions){
                var question = new Question
                {
                    QuestionTemplate = new Collection<Template>(),
                    Name = viewModelQuestion.Key,
                    Type = viewModelQuestion.Type,
                    Version = viewModelQuestion.Version
                };
                
                var questionRule = new QuestionRule
                {
                    ExpressionModel = new Collection<ExpressionModel>(),
                    Name = viewModelQuestion.Key,
                    HideQuestion = viewModelQuestion.HideExpression
                };
                
                if(viewModelQuestion.ExpressionProperties != null){
                    foreach(var (key, value) in viewModelQuestion.ExpressionProperties){
                        var expressionModel = new ExpressionModel
                        {
                            Key = key, 
                            Expression = value
                        };
                        questionRule.ExpressionModel.Add(expressionModel);
                    }
                }

                var globalTemplate = viewModelQuestion.TemplateOptions;
                
                foreach(var translation in globalTemplate.Translations){
                        // check if question is required
                        questionRule.Required = globalTemplate.Required;

                        // Add all templateswith different languages to the template
                        var questionTemplate = new Template
                        {
                            Description = translation.Description,
                            InputType = globalTemplate.Type,
                            Label = translation.Label,
                            Language = translation.Language,
                            Placeholder = translation.Placeholder,
                            Validations = new Collection<Validation>()
                        };
                        
                        if(translation.Validation.Messages != null){
                            foreach(var (key, value) in translation.Validation.Messages){
                                var expressionModel = new Validation()
                                {
                                    Type = key, 
                                    Message = value
                                };
                                questionTemplate.Validations.Add(expressionModel);
                            }
                        }

                        if(translation.MultipleChoiceOptions != null){
                            
                            questionTemplate.SelectAllOption = translation.SelectAllOption;
                            questionTemplate.AllowedToSelectMultipleOptions = globalTemplate.Multiple;
                            questionTemplate.Options = new Collection<MultipleChoiceOption>();

                            foreach(var option in translation.MultipleChoiceOptions){
                                var multipleChoiceOption = new MultipleChoiceOption
                                {
                                    Value = option.Value, 
                                    Label = option.Label
                                };
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
