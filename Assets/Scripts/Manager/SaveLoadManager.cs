using UnityEngine;

public static class SaveLoadManager
{
    #region LEVEL

    const string KEY_LEVEL = "levels";

    public static void IncreaseLevel() => PlayerPrefs.SetInt(KEY_LEVEL, GetLevel() + 1);
    public static int GetLevel() => PlayerPrefs.GetInt(KEY_LEVEL, 0);

    #endregion
    
    
    #region ROW_COUNT

    const string KEY_ROW_COUNT = "rowCount";

    public static void IncreaseRowNumber() => PlayerPrefs.SetInt(KEY_ROW_COUNT, GetRowCount() + 1); 
    public static int GetRowCount() => PlayerPrefs.GetInt(KEY_ROW_COUNT, 2);

    #endregion
    
    #region COLUMN_COUNT

    const string KEY_COLUMN_COUNT = "columnCount";

    public static void IncreaseColumnNumber() => PlayerPrefs.SetInt(KEY_COLUMN_COUNT, GetColumnCount() + 1);
    public static int GetColumnCount() => PlayerPrefs.GetInt(KEY_COLUMN_COUNT, 2);

    #endregion
    
    #region LIGHT_PERCENTAGE

    const string KEY_LIGHT_PERCENTAGE = "lightPercentage";

    public static void IncreaseLightPercentage() => PlayerPrefs.SetInt(KEY_LIGHT_PERCENTAGE, GetLightPercentage() + 5); 
    public static int GetLightPercentage() => PlayerPrefs.GetInt(KEY_LIGHT_PERCENTAGE, 10);

    #endregion
}
