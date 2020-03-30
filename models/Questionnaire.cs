using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace questionnaireBackend
{
    public class Questionnaire
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id {get; set;}
        public string Category {get; set;}
        public string Version {get; set;}
        public Collection<Question> Questions {get; set;}
        public Collection<QuestionRule> QuestionRules {get; set;}
    }

    public class QuestionRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id {get; set;}
        public string Name {get; set;}
        public bool Required {get; set;}
        public string HideQuestion {get; set;}
        public Collection<ExpressionModel> ExpressionModel {get; set;}
    }

    public class ExpressionModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id {get; set;}
        public string Key {get; set;}
        public string Expression {get; set;}
    }
    
    public class Validation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id {get; set;}
        public string Type {get; set;}
        public string Message {get; set;}
    }

    public class Question
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id {get; set;}
        public string Name {get; set;}
        public string Version {get; set;}
        public string Type {get; set;}
        public Collection<Template> QuestionTemplate {get; set;}
    }

    public class Template
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id {get; set;}
        public string Language {get; set;}
        public string Description {get; set;}
        public string InputType {get; set;}
        public string Label {get; set;}
        public string Answer {get; set;}
        public string Placeholder {get; set;}
        public string SelectAllOption {get; set;}
        public bool AllowedToSelectMultipleOptions {get; set;}
        public Collection<MultipleChoiceOption> Options {get; set;}
        public Collection<Validation> Validations {get; set;}
    }

    public class MultipleChoiceOption
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id {get; set;}
        public int Value {get; set;}
        public string Label {get; set;}
    }

}

