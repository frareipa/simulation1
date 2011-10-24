using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultipleQueue.Simulation;
namespace MultipleQueue
{
    class SimulationManager
    {
        MultipleQueue.Simulation.Model model;

        public MultipleQueue.Simulation.Model Model
        {
            get { return this.model; }
        }
        public void StartSimulation()
        {
            throw new NotImplementedException(@"Implement StartSimulation in SimulationManager.cs");

            //get all inputs from the user
            //create the model
            model = new Model();
             model.Run();
            //start running the simulation
        }


    }
}
