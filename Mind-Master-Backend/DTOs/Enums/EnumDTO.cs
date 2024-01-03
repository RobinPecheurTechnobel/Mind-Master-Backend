namespace Mind_Master_Backend.DTOs.Enums
{
    /// <summary>Classe universelle pour le transfert d'un enum dans une forme lisible</summary>
    public class EnumDTO
    {
        /// <summary>Champ renvoyant la clé de l'élément choisit</summary>
        public int Key { get { return Convert.ToInt32(_enum); } }

        /// <summary>Champ renvoyant la valeur de l'élément choisit</summary>
        public string Name { get { return _enum.ToString(); } }

        /// <summary>Valeur choisit de l'enum</summary>
        private Enum _enum;

        /// <summary>Constructeur initialisant la valeur d'enum dont fait référence cet objet</summary>
        /// <param name="inputEnum">Valeur à stocker</param>
        public EnumDTO(Enum inputEnum)
        {
            _enum = inputEnum;
        }
    }
}
