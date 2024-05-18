using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.EntityCreatingBeforeStart;

namespace OOP_ICT.Fifth.BankMenuUI;

public class CreateBankAccountCommandHandler
{
    public void Handle(CreateBankAccountCommand command)
    {
        BankCreationMenuView.Banks[ConstantNumbers.IndexOfBankWeAreIn].CreateNewAccount(command.Player);
    }
}