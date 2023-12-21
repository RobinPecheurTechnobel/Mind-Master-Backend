namespace Mind_Master_Backend.Mappers
{
    /// <summary>Class permettant d'ajouter une fonction pour les enums de manière générique</summary>
    /// <typeparam name="T">Forme générique acceptant les enums</typeparam>
    public class EnumMapper<T> where T : Enum
    {
        /// <summary>Fonction d'extension permet de sortir les valeurs d'une enumeration sous la form d'un IEnumerable</summary>
        /// <returns>IEnumerable de l'enumeration</returns>
        public static IEnumerable<T> GetAllValuesAsIEnumerable()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
