using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Data;
using System.Collections.Generic;
using SolidWorks.Interop.swconst;
using System.Linq;


namespace SolidWorks.API.BoxingSW
{
    public class SwDocumentProperty
    {
        private Dictionary<string,List<SwProperty>> _ConfigPropertys;
        private CustomPropertyManager _customPropManager;

        public ModelDoc2 SwModel { get; }

        public SwDocumentProperty(ModelDoc2 swModel)
        {
            SwModel = swModel;
            _ConfigPropertys = new Dictionary<string, List<SwProperty>>();
            SetPropertysConfig();
        }

       
        /// <summary>
        /// Заполнение свойств заданной конфигурации
        /// </summary>
        /// <param name="confgName"></param>
        private void SetProperty(string confgName)
        {
            object vPropNames = null;
            string[] propNames;
            object vPropTypes = null;
            object vPropValues = null;
            object[] propValues;
            object resolved = null;
            object linkProp = null;
            int CountPropertyes;

            _customPropManager = SwModel.Extension.CustomPropertyManager[confgName];


            // Get the custom properties
            CountPropertyes = _customPropManager.GetAll3(ref vPropNames,
                ref vPropTypes,
                ref vPropValues,
                ref resolved,
                ref linkProp);

            propValues = (object[])vPropValues;            
            propNames = (string[])vPropNames;

            // Заполняем лист свойств 
            List<SwProperty> listProp = new List<SwProperty>();
            for (int i = 0; i < CountPropertyes; i++)
            {
                listProp.Add( new SwProperty( propNames[ i ], propValues[ i ], swCustomInfoType_e.swCustomInfoText ) );
            }

            _ConfigPropertys.Add( confgName, listProp );                      
        }


        /// <summary>
        /// Заполнение свойств всех имеющихся конфигураций
        /// </summary>
        private void SetPropertysConfig()
        {
            string[] configNames = null;
            SetProperty( "" ); // Добавление общих свойств документа
            configNames = (string[])SwModel.GetConfigurationNames();

            if (configNames != null)
            {
                // Добавление свойств каждой конфигурации
                foreach (string configName in configNames)
                {
                    SetProperty( configName );
                }
            }
        }


    #region *** Public method ***
        /// <summary>
        /// Возвращает список конфигураций модели
        /// </summary>
        /// <returns> List<string> </returns>
        public List<string> GetConfiguration()
        {
            List<string> resultList = new List<string>();
            foreach (var item in _ConfigPropertys.Keys)
            {
                resultList.Add( item );
            }
            return resultList;
        }

        /// <summary>
        /// Возвращает список свойств заданной конфигурации
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<SwProperty> GetPropertys(string name )
        {
            try
            {
                return _ConfigPropertys[ name ];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// Метод добавляет свойство в соответствующую конфигурацию
        /// объекта и в ModelDoc2
        /// </summary>
        /// <param name="nameConfig"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public int Add(string nameConfig , SwProperty prop )
        {
           

            if (prop == null)
            {
                throw new System.ArgumentNullException( nameof( prop ) );
            }

            if ( !_ConfigPropertys.ContainsKey( nameConfig ) )
            {
                throw new KeyNotFoundException( nameof( prop ) );
            }

            // Выбираем исполнение в котором собираемся менять свойства
            _customPropManager = SwModel.Extension.CustomPropertyManager[ nameConfig ];

            //Если свойство уже имеется в списке
            // изменить его значение Value, иначе добавить в список.
            if (_ConfigPropertys[ nameConfig ].Contains( prop )) 
            {
                _ConfigPropertys[ nameConfig ].First( x => x.Name == prop.Name ).Value = prop.Value;
            }
            else
            {
                _ConfigPropertys[ nameConfig ].Add( prop );
            }

            //Добавляем свойство в модель SolidWorks
            return _customPropManager.Add3( prop.Name, (int)swCustomInfoType_e.swCustomInfoText,
                                    prop.Value, (int)swCustomPropertyAddOption_e.swCustomPropertyReplaceValue );


        }

        /// <summary>
        /// Метод удаляет свойство в соответствующую конфигурацию
        /// объекта и в ModelDoc2
        /// </summary>
        /// <param name="nameConfig"></param>
        /// <param name="prop"></param>
        /// <returns></returns>
        public int Remove( string nameConfig, SwProperty prop )
        {
            if (prop == null)
            {
                throw new System.ArgumentNullException( nameof( prop ) );
            }

            if (!_ConfigPropertys.ContainsKey( nameConfig ))
            {
                throw new KeyNotFoundException( nameof( prop ) );
            }

            // Выбираем исполнение в котором собираемся менять свойства
            _customPropManager = SwModel.Extension.CustomPropertyManager[ nameConfig ];

            //Удаляем из списка свойств.
            if (_ConfigPropertys[ nameConfig ].Remove( prop ))
            {
                //Удаляем свойство из модели SolidWorks
                return _customPropManager.Delete2( prop.Name );
            }
            else
            {
                return 1;
            }
        }
        #endregion

        //public void ApplyChangePropertyModel(string tableName)
        //{
        //    // Проходимся по всем строкам в таблице, проверяя свойства состояния
        //    // в соответствии с этим значение добавляем, удаляем, изменяем свойства в модели SolidWorks
        //    string tblName = tableName;
        //    _customPropManager = SwModel.Extension.CustomPropertyManager[tableName.Contains("Настройки") ? tblName = "" : tblName = tableName];

        //    foreach (DataRow row in dataSet.Tables[tableName].Rows)
        //    {
        //        switch (row.RowState)
        //        {
        //            case DataRowState.Detached:
        //                break;
        //            case DataRowState.Unchanged:
        //                break;
        //            case DataRowState.Added:
        //                _customPropManager.Add3(row["Имя свойства"] as string, 30, row["Значение"] as string, 0);
        //                break;
        //            case DataRowState.Deleted:
        //                _customPropManager.Delete2(row["Имя свойства", DataRowVersion.Original] as string);
        //                break;
        //            case DataRowState.Modified:
        //                _customPropManager.Set2(row["Имя свойства"] as string, row["Значение"] as string);
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    dataSet.Tables[tableName].AcceptChanges();
        //}

    }
}

