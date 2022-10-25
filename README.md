# SpecificationEngine
Framwork base on Specification pattern

How to use
Main part is Specification class. Specification is a like business condition. For example if you have in description Everywhere who has more than 30 year will have discount 50%, then you can create specification PersonWhoHasMoreThan30Years.
This specification can be expressed in code by Specification.Create((Person person, context) =>  (DateTime.Today.Year - person.DateBirth.Year) > 30);

It means specification takes in parameter input object and return some anonymous method which return true or false. 
Specification overrides & | ! operators so each specification can be combined with these operators together and create more complex condition. 

var ageMoreThan30 = Specification.Create((Person person, context) =>  (DateTime.Today.Year - person.DateBirth.Year) > 30);

var ageLessThan60 = Specification.Create((Person person, context) =>  (DateTime.Today.Year - person.DateBirth.Year) < 60);

var ageMoreThan30AndLessThan60 = ageMoreThan30 & ageLessThan60;

Second main part is Rule. Rule takes Specification and there can be more options what it can do. 

You can have validation rule if you fill ErrorMessage parameter. ErrorMessage can be created similarly as Specification. It means you can create ErrorMessage by string but there is possibility to create method which return some string or Message object. You can compose error message as you can.

There exists Message type which hold error message. You can inherit from Message type and create your own Message type (i.e. MyMessage). 

public class MyMessage : Message
{
    public MyMessage(string message, MessageTye messageType, int meessageNumber) : base(message, messageType)
    {
        MessageNumber = messageNumber;
    }
    
    public int MessageNumber { get; private set; }
}

If you return MyMessage instead of Message object from ErrorMessage function you can give your Message as you need it in your application.

You can have action rule if you fill SuccessAction or FailureAction parameter. Rule evaluator will invoke SuccessAction when Specification is true otherwise it will launch FailureAction.
