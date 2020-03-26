using System.Collections;
using System.Collections.ObjectModel;

namespace questionnaireBackend
{
    public class Questionnaire
    {
        public string Id {get; set;}
        public string Category {get; set;}
        public string Version {get; set;}
        public Collection<Question> Questions {get; set;}
        public Collection<QuestionRules> QuestionRules {get; set;}
    }

    public class QuestionRules 
    {
        public string Id {get; set;}
        public bool Required {get; set;}
        public string HideQuestion {get; set;}
        public string ExpressionProperty {get; set;}
        public string HideQuestionWhen(Question dependentQuestion, Template value){
            return "";
        }
        public string CreateExpressionProperty(string modelName){
            return "";
        }

    }

    public class Question
    {
        public string Id {get; set;}
        public string Name {get; set;}
        public string Version {get; set;}
        public string Type {get; set;}
        public ArrayList[] QuestionTemplate {get; set;}
    }

    public class Template
    {
        public string Id {get; set;}
        public string Language {get; set;}
        public string Description {get; set;}
        public string Version {get; set;}
        public string InputType {get; set;}
        public string Label {get; set;}
        public string Answer {get; set;}
        public string Placeholder {get; set;}
    }

    public class MultipleChoiceTemplate : Template 
    {
        public string SelectAllOption {get; set;}
        public Collection<MultipleChoiceOption> Options {get; set;}
    }

    public class MultipleChoiceOption
    {
        public string Id {get; set;}
        public int Value {get; set;}
        public string Label {get; set;}
    }

}

