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
            foreach(var viewModelQuestion in _questionnaireViewModel.Schema){
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

                foreach(var template in viewModelQuestion.TemplateOptions){
                        // check if question is required
                        questionRule.Required = template.Required;

                        // Add all templateswith different languages to the template
                        var questionTemplate = new Template
                        {
                            Description = template.Description,
                            InputType = template.Type,
                            Label = template.Label,
                            Language = template.Language,
                            Placeholder = template.Placeholder,
                            Version = template.Version
                        };

                        if(template.Options != null){
                            
                            questionTemplate.SelectAllOption = template.selectAllOption;
                            questionTemplate.MultipleChoiceQuestion = template.multiple;
                            questionTemplate.Options = new Collection<MultipleChoiceOption>();

                            foreach(var option in template.Options){
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
