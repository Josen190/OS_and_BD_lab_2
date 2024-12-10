using System.Data;
using System.Xml.Linq;

namespace OS_and_BD_lab_2.Models.CodeRepositroy.D
{
    public class CodeRepositroyD : BaseCodeRepository<DataInputD, DataOutputD>
    {
        public override DataOutputD run(DataInputD input_)
        {

            // Начальная позиция удава
            List<(int, int)> boa = new List<(int, int)>();
            for (int i = 1; i <= input_.boaLength; i++)
            {
                boa.Add((i, 1));
            }

            int k = 0;
            (int, int) forwardVec = (1, 0);

            foreach (char movement in input_.movements)
            {
                k++;
                (int, int) newHeadPosition;

                if (movement == 'F')
                {
                    newHeadPosition = (boa.Last().Item1 + forwardVec.Item1, boa.Last().Item2 + forwardVec.Item2);
                }
                else if (movement == 'R')
                {
                    var rightVec = (-forwardVec.Item2, forwardVec.Item1);
                    newHeadPosition = (boa.Last().Item1 + rightVec.Item1, boa.Last().Item2 + rightVec.Item2);
                    forwardVec = rightVec;
                }
                else
                {
                    var leftVec = (forwardVec.Item2, -forwardVec.Item1);
                    newHeadPosition = (boa.Last().Item1 + leftVec.Item1, boa.Last().Item2 + leftVec.Item2);
                    forwardVec = leftVec;
                }

                // Проверки на препятствия и границы
                if (input_.obstacles.Contains(newHeadPosition) ||
                    boa.Skip(1).Contains(newHeadPosition) ||
                    (newHeadPosition == boa[0] && k == 9) ||
                    newHeadPosition.Item1 == 0 || newHeadPosition.Item1 == 201 ||
                    newHeadPosition.Item2 == 0 || newHeadPosition.Item2 == 201)
                {
                    break;
                }
                else
                {
                    boa.Add(newHeadPosition);
                }

                if (k != 9)
                {
                    boa.RemoveAt(0);
                }
                else
                {
                    k = 0;
                }
            }
            DataOutputD output = new DataOutputD();
            output.boa = boa[0];
            return output;
        }
    }
}
