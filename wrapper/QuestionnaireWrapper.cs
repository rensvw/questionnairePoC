using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using questionnaireBackend.models;

namespace questionnaireBackend.wrapper
{
    public class QuestionnaireWrapper : IQuestionnaireWrapper
    {
        private Questionnaire _questionnaireStoreModel = new Questionnaire();
        private readonly QuestionnairePostViewModel _questionnairePostViewModel = new QuestionnairePostViewModel();
        private readonly QuestionnaireGetViewModel _questionnaireGetViewModel = new QuestionnaireGetViewModel();

        public QuestionnaireWrapper(QuestionnairePostViewModel questionnairePostViewModel)
        {
            this._questionnairePostViewModel = questionnairePostViewModel;
        }

        public QuestionnaireWrapper(Questionnaire questionnaireStoreModel)
        {
            this._questionnaireStoreModel = questionnaireStoreModel;
        }

        public List<AssetQuestionModel> GetAllAnsweredQuestions()
        {
            throw new System.NotImplementedException();
        }

        public void SubstituteAnsweredQuestion(int questionId, string answer)
        {
            throw new NotImplementedException();
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
                Category = _questionnairePostViewModel.Category,
                Version = _questionnairePostViewModel.Version,
                Questions = new Collection<Question>(),
                QuestionRules = new Collection<QuestionRule>()
            };

            // create stopre model questions and questionrules
            foreach (var viewModelQuestion in _questionnairePostViewModel.Questions)
            {
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

                if (viewModelQuestion.ExpressionProperties != null)
                {
                    foreach (var (key, value) in viewModelQuestion.ExpressionProperties)
                    {
                        var expressionModel = new ExpressionModel
                        {
                            Key = key,
                            Expression = value
                        };
                        questionRule.ExpressionModel.Add(expressionModel);
                    }
                }

                var globalTemplate = viewModelQuestion.TemplateOptions;

                foreach (var translation in globalTemplate.Translations)
                {
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

                    if (translation.Validation.Messages != null)
                    {
                        foreach (var (key, value) in translation.Validation.Messages)
                        {
                            var expressionModel = new Validation()
                            {
                                Type = key,
                                Message = value
                            };
                            questionTemplate.Validations.Add(expressionModel);
                        }
                    }

                    if (translation.MultipleChoiceOptions != null)
                    {

                        questionTemplate.SelectAllOption = translation.SelectAllOption;
                        questionTemplate.AllowedToSelectMultipleOptions = globalTemplate.Multiple;
                        questionTemplate.Options = new Collection<MultipleChoiceOption>();

                        foreach (var option in translation.MultipleChoiceOptions)
                        {
                            var multipleChoiceOption = new MultipleChoiceOption
                            {
                                Value = option.Value,
                                Label = option.Label
                            };
                            questionTemplate.Options.Add(multipleChoiceOption);
                        }

                        question.QuestionTemplate.Add(questionTemplate);
                    }
                    else
                    {
                        question.QuestionTemplate.Add(questionTemplate);
                    }
                }

                questionnaire.Questions.Add(question);
                questionnaire.QuestionRules.Add(questionRule);
            }

            return questionnaire;
        }

        public QuestionnaireGetViewModel GetViewModel(string language)
        {
            var questionnaire = new QuestionnaireGetViewModel()
            {
                Id = _questionnaireStoreModel.Id,
                Category = _questionnaireStoreModel.Category,
                Version = _questionnaireStoreModel.Version,
                Questions = new Collection<FormlyQuestionGetView>(),
            };

            foreach (var question in _questionnaireStoreModel.Questions)
            {
                var viewModelQuestion = new FormlyQuestionGetView()
                {
                    Id = question.Id,
                    Key = question.Name,
                    Type = question.Type,
                    Version = question.Version,
                    TemplateOptions = new TemplateOptionsGetView()
                };
                foreach (var template in question.QuestionTemplate)
                {
                    if (String.Equals(template.Language, language, StringComparison.CurrentCultureIgnoreCase))
                    {
                        viewModelQuestion.TemplateOptions.Id = template.Id;
                        viewModelQuestion.TemplateOptions.Description = template.Description;
                        viewModelQuestion.TemplateOptions.Label = template.Label;
                        viewModelQuestion.TemplateOptions.Language = template.Language;
                        viewModelQuestion.TemplateOptions.Placeholder = template.Placeholder;
                        viewModelQuestion.TemplateOptions.Type = template.InputType;
                        viewModelQuestion.TemplateOptions.Multiple = template.AllowedToSelectMultipleOptions;
                        viewModelQuestion.TemplateOptions.SelectAllOption = template.SelectAllOption;
                        viewModelQuestion.TemplateOptions.Options = new Collection<FormlyOption>();
                        
                        foreach (var options in template.Options)
                        {
                            var option = new FormlyOption()
                            {
                                Id = options.Id,
                                Value = options.Value,
                                Label = options.Label
                            };
                            viewModelQuestion.TemplateOptions.Options.Add(option);
                        }
                        
                        viewModelQuestion.ExpressionProperties = new Dictionary<string, string>();
                        questionnaire.Questions.Add(viewModelQuestion);
                        
                    }
                }

                foreach (var questionRule in _questionnaireStoreModel.QuestionRules)
                {
                    foreach (var viewQuestion in questionnaire.Questions)
                    {
                        if (viewQuestion.Key.ToLower() == questionRule.Name.ToLower())
                        {
                            if(questionRule.ExpressionModel.Count != 0){

                                foreach(var y in questionRule.ExpressionModel){
                                    viewQuestion.ExpressionProperties.Add(y.Key,y.Expression);
                                }

                            }
                            viewQuestion.HideExpression = questionRule.HideQuestion;
                            viewQuestion.Required = questionRule.Required;
                        }
                    }
                }


            }
            return questionnaire;

        }

    }
}
