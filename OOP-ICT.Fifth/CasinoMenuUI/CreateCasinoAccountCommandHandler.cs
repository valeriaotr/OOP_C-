using OOP_ICT.Fifth.Constants;
using OOP_ICT.Fifth.EntityCreatingBeforeStart;
namespace OOP_ICT.Fifth.CasinoMenuUI;

public class CreateCasinoAccountCommandHandler
{
    public void Handle(CreateCasinoAccountCommand command)
    {
        CasinoCreationMenuView.Casinos[ConstantNumbers.IndexOfCasinoWeAreIn]
            .CreateNewCasinoAccount(command.Player);
    }
}