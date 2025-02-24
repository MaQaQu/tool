﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouiToolkit.Models
{
    public sealed class PageMaintainModel : ViewModelBase
    {
        private static PageMaintainModel _PageMaintainModel = null;
        static PageMaintainModel()
        {
            _PageMaintainModel = new PageMaintainModel();
        }
        private PageMaintainModel()
        {
            videoPlayingFlag = false;
            navDataDownloadStep = 0;
            downloadingFlag = false;
            navDataDeleteStep = 0;
            deletingFlag = false;
            navDataOpenStep = 0;
            openingFlag = false;
            overDownloadFlag = false;
            overDeleteFlag = false;
            overDownloadConfirmFlag = false;
            overDeleteConfirmFlag = false;
            downloadSuccessFlag = true;
            deleteSuccessFlag = true;
            openSuccessFlag = true;
            updateTimeBarFlag = false;
            InitDtNavFilesName();
            InitDtNavData();
            InitDtPointCloudData();
            InitDtAlarmData();
            InitDtSpeedData();
        }
        public static PageMaintainModel CreateInstance()
        {
            return _PageMaintainModel;
        }
        private void InitDtNavFilesName()
        {
            dtNavFilesName = new DataTable("simulatenavfilesname");
            //dt新增列
            dtNavFilesName.Columns.Add("ID", Type.GetType("System.Int32"));
            dtNavFilesName.Columns.Add("FilesName", Type.GetType("System.String"));
            dtNavFilesName.Columns.Add("Date", Type.GetType("System.String"));
            dtNavFilesName.Columns.Add("StartTime", Type.GetType("System.String"));
            dtNavFilesName.Columns.Add("EndTime", Type.GetType("System.String"));
            dtNavFilesName.Columns.Add("Remark", Type.GetType("System.String"));
        }
        private void InitDtNavData()
        {
            dtNavData = new DataTable("simulatenavdata");
            DataColumn dc = null;
            //dt新增列
            dc = dtNavData.Columns.Add("ID", Type.GetType("System.Int32")); //ID
            dc = dtNavData.Columns.Add("VehicleID", Type.GetType("System.Int32")); //VehicleID
            dc = dtNavData.Columns.Add("CurrentTime", Type.GetType("System.DateTime")); //CurrentTime
            dc = dtNavData.Columns.Add("NavData", Type.GetType("System.String")); //NavData
            dc = dtNavData.Columns.Add("Remark", Type.GetType("System.String")); //Remark
        }
        private void InitDtPointCloudData()
        {
            dtPointCloudData = new DataTable("simulatepointcloud");
            DataColumn dc = null;
            //dt新增列
            dc = dtPointCloudData.Columns.Add("ID", Type.GetType("System.Int32"));
            dc = dtPointCloudData.Columns.Add("VehicleID", Type.GetType("System.Int32"));
            dc = dtPointCloudData.Columns.Add("TotalCount", Type.GetType("System.Int32"));
            dc = dtPointCloudData.Columns.Add("CurrentCount", Type.GetType("System.Int32"));
            dc = dtPointCloudData.Columns.Add("XArray", Type.GetType("System.String"));
            dc = dtPointCloudData.Columns.Add("YArray", Type.GetType("System.String"));
            dc = dtPointCloudData.Columns.Add("CurrentTime", Type.GetType("System.DateTime"));
        }
        private void InitDtAlarmData()
        {
            dtAlarmData = new DataTable("simulatealarmdata");
            dtAlarmData.Columns.Add("ID", Type.GetType("System.Int32"));
            dtAlarmData.Columns.Add("VechicleID", Type.GetType("System.Int32"));
            dtAlarmData.Columns.Add("AlarmCode", Type.GetType("System.Int32"));
            dtAlarmData.Columns.Add("StartTime", Type.GetType("System.DateTime"));
            dtAlarmData.Columns.Add("EndTime", Type.GetType("System.DateTime"));
        }
        private void InitDtSpeedData()
        {
            dtSpeedData = new DataTable("simulatespeeddata");
            dtSpeedData.Columns.Add("ID", Type.GetType("System.Int32"));
            dtSpeedData.Columns.Add("VechicleID", Type.GetType("System.Int32"));
            dtSpeedData.Columns.Add("Speed", Type.GetType("System.Int32"));
            dtSpeedData.Columns.Add("Unit", Type.GetType("System.String"));
            dtSpeedData.Columns.Add("Speed_X", Type.GetType("System.Int32"));
            dtSpeedData.Columns.Add("Unit_X", Type.GetType("System.String"));
            dtSpeedData.Columns.Add("Speed_Y", Type.GetType("System.Int32"));
            dtSpeedData.Columns.Add("Unit_Y", Type.GetType("System.String"));
            dtSpeedData.Columns.Add("Speed_W", Type.GetType("System.Int32"));
            dtSpeedData.Columns.Add("Unit_W", Type.GetType("System.String"));
            dtSpeedData.Columns.Add("CurrentTime", Type.GetType("System.DateTime"));
        }

        public bool videoPlayingFlag { get; set; }
        public int navDataDownloadStep { get; set; }
        public bool downloadingFlag { get; set; }
        public int navDataDeleteStep { get; set; }
        public bool deletingFlag { get; set; }
        public int navDataOpenStep { get; set; }
        public bool openingFlag { get; set; }
        public bool overDownloadFlag { get; set; }
        public bool overDownloadConfirmFlag { get; set; }
        public bool overDeleteFlag { get; set; }
        public bool overDeleteConfirmFlag { get; set; }
        public bool overOpenFlag { get; set; }
        public bool downloadSuccessFlag { get; set; }
        public bool deleteSuccessFlag { get; set; }
        public bool openSuccessFlag { get; set; }
        public bool updateTimeBarFlag { get; set; }
        public DataTable dtNavData { get; set; }
        public DataTable dtPointCloudData { get; set; }
        public DataTable dtNavFilesName { get; set; }
        public DataTable dtAlarmData { get; set; }
        public DataTable dtSpeedData { get; set; }
        public string strNavDataCacheFilePath { get; set; }
    }
}
