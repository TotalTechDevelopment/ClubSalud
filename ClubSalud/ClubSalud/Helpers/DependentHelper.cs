using System;
using System.Collections.Generic;

namespace ClubSalud.Helpers
{
    public class DependentHelper
    {
        public static DetalleDeDependientesDeUsuario CurrentDependent { get; set; }
        public static int CurrentDependentPosition { get; set; }
        public static List<DetalleDeDependientesDeUsuario> ListaDependientes { get; set; }
    }
}
