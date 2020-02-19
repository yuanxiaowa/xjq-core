using Dm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OdyLibrary
{
    public class MyDm
    {
        dmsoft dm = new dmsoft();
        public string Ver()
        {
            return dm.Ver();
        }

        public int SetPath(string path)
        {
            return dm.SetPath(path);
        }

        public string Ocr(int x1, int y1, int x2, int y2, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public Point FindStr(int x1, int y1, int x2, int y2, string str, string color, double sim = 0.9)
        {
            object x;
            object y;
            dm.FindStr(x1, y1, x2, y2, str, color, sim, out x, out y);
            return new Point((int)x, (int)y);
        }

        public int GetResultCount(string str)
        {
            return dm.GetResultCount(str);
        }

        public int GetResultPos(string str, int index, out object x, out object y)
        {
            throw new NotImplementedException();
        }

        public int StrStr(string s, string str)
        {
            return dm.StrStr(s, str);
        }

        public int SendCommand(string cmd)
        {
            return SendCommand(cmd);
        }

        public int UseDict(int index)
        {
            return dm.UseDict(index);
        }

        public string GetBasePath()
        {
            return GetBasePath();
        }

        public int SetDictPwd(string pwd)
        {
            return dm.SetDictPwd(pwd);
        }

        public string OcrInFile(int x1, int y1, int x2, int y2, string pic_name, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public int Capture(int x1, int y1, int x2, int y2, string file)
        {
            return dm.Capture(x1, y1, x2, y2, file);
        }

        public int KeyPress(Keys vk)
        {
            return dm.KeyPress((int)vk);
        }

        public int KeyDown(Keys vk)
        {
            return dm.KeyDown((int)vk);
        }

        public int KeyUp(Keys vk)
        {
            return dm.KeyUp((int)vk);
        }

        public int LeftClick()
        {
            return dm.LeftClick();
        }

        public int RightClick()
        {
            return dm.RightClick();
        }

        public int MiddleClick()
        {
            return dm.MiddleClick();
        }

        public int LeftDoubleClick()
        {
            return dm.LeftDoubleClick();
        }

        public int LeftDown()
        {
            return dm.LeftDown();
        }

        public int LeftUp()
        {
            return dm.LeftUp();
        }

        public int RightDown()
        {
            return dm.RightDown();
        }

        public int RightUp()
        {
            return dm.RightUp();
        }

        public int MoveTo(int x, int y)
        {
            return dm.MoveTo(x, y);
        }

        public int MoveR(int rx, int ry)
        {
            return dm.MoveR(rx, ry);
        }

        public string GetColor(int x, int y)
        {
            return dm.GetColor(x, y);
        }

        public string GetColorBGR(int x, int y)
        {
            return dm.GetColorBGR(x, y);
        }

        public string RGB2BGR(string rgb_color)
        {
            throw new NotImplementedException();
        }

        public string BGR2RGB(string bgr_color)
        {
            throw new NotImplementedException();
        }

        public int UnBindWindow()
        {
            return dm.UnBindWindow();
        }

        public int CmpColor(int x, int y, string color, double sim = 0.9)
        {
            return dm.CmpColor(x, y, color, sim);
        }

        public Point ClientToScreen(int hwnd, int x, int y)
        {
            object x1 = x;
            object y1 = y;
            dm.ClientToScreen(hwnd, ref x1, ref y1);
            return new Point((int)x1, (int)y1);
        }

        public Point ScreenToClient(int hwnd, int x, int y)
        {
            object x1 = x;
            object y1 = y;
            dm.ScreenToClient(hwnd, ref x1, ref y1);
            return new Point((int)x1, (int)y1);
        }

        public int ShowScrMsg(int x1, int y1, int x2, int y2, string msg, string color)
        {
            throw new NotImplementedException();
        }

        public int SetMinRowGap(int row_gap)
        {
            throw new NotImplementedException();
        }

        public int SetMinColGap(int col_gap)
        {
            throw new NotImplementedException();
        }

        public Point FindColor(int x1, int y1, int x2, int y2, string color, double sim = 0.9, int dir = 0)
        {
            object x;
            object y;
            dm.FindColor(x1, y1, x2, y2, color, sim, dir, out x, out y);
            return new Point((int)x, (int)y);
        }

        public string FindColorEx(int x1, int y1, int x2, int y2, string color, double sim, int dir)
        {
            throw new NotImplementedException();
        }

        public int SetWordLineHeight(int line_height)
        {
            throw new NotImplementedException();
        }

        public int SetWordGap(int word_gap)
        {
            throw new NotImplementedException();
        }

        public int SetRowGapNoDict(int row_gap)
        {
            throw new NotImplementedException();
        }

        public int SetColGapNoDict(int col_gap)
        {
            throw new NotImplementedException();
        }

        public int SetWordLineHeightNoDict(int line_height)
        {
            throw new NotImplementedException();
        }

        public int SetWordGapNoDict(int word_gap)
        {
            throw new NotImplementedException();
        }

        public int GetWordResultCount(string str)
        {
            throw new NotImplementedException();
        }

        public int GetWordResultPos(string str, int index, out object x, out object y)
        {
            throw new NotImplementedException();
        }

        public string GetWordResultStr(string str, int index)
        {
            throw new NotImplementedException();
        }

        public string GetWords(int x1, int y1, int x2, int y2, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public string GetWordsNoDict(int x1, int y1, int x2, int y2, string color)
        {
            throw new NotImplementedException();
        }

        public int SetShowErrorMsg(int show)
        {
            return dm.SetShowErrorMsg(show);
        }

        public Point GetClientSize(int hwnd)
        {
            object x;
            object y;
            dm.GetClientSize(hwnd, out x, out y);
            return new Point((int)x, (int)y);
        }

        public int MoveWindow(int hwnd, int x, int y)
        {
            throw new NotImplementedException();
        }

        public string GetColorHSV(int x, int y)
        {
            throw new NotImplementedException();
        }

        public string GetAveRGB(int x1, int y1, int x2, int y2)
        {
            throw new NotImplementedException();
        }

        public string GetAveHSV(int x1, int y1, int x2, int y2)
        {
            throw new NotImplementedException();
        }

        public int GetForegroundWindow()
        {
            return GetForegroundWindow();
        }

        public int GetForegroundFocus()
        {
            return GetForegroundFocus();
        }

        public int GetMousePointWindow()
        {
            return dm.GetMousePointWindow();
        }

        public int GetPointWindow(int x, int y)
        {
            return dm.GetPointWindow(x, y);
        }
        public delegate bool LpfnDelegate(int a1, int a2);
        public string EnumWindow(int parent, string title, string class_name, int filter)
        {
            return dm.EnumWindow(parent, title, class_name, filter);
        }

        [DllImport("user32.dll")]

        //EnumWindows函数，EnumWindowsProc 为处理函数

        public static extern int EnumWindows(LpfnDelegate lpfn, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(int hWndParent, LpfnDelegate lpfn, int lParam);

        public int GetWindowState(int hwnd, int flag)
        {
            return dm.GetWindowState(hwnd, flag);
        }

        public int GetWindow(int hwnd, int flag)
        {
            throw new NotImplementedException();
        }

        public int GetSpecialWindow(int flag)
        {
            return dm.GetSpecialWindow(flag);
        }

        public int SetWindowText(int hwnd, string text)
        {
            return dm.SetWindowText(hwnd, text);
        }

        public int SetWindowSize(int hwnd, int width, int height)
        {
            return dm.SetWindowSize(hwnd, width, height);
        }

        public Rectangle GetWindowRect(int hwnd)
        {
            object x1 = 0;
            object y1 = 0;
            object x2 = 0;
            object y2 = 0;
            dm.GetWindowRect(hwnd, out x1, out y1, out x2, out y2);
            return new Rectangle((int)x1, (int)y1, (int)x2 - (int)x1, (int)y2 - (int)y1);
        }

        public string GetWindowTitle(int hwnd)
        {
            return dm.GetWindowTitle(hwnd);
        }

        public string GetWindowClass(int hwnd)
        {
            return dm.GetWindowClass(hwnd);
        }

        public int SetWindowState(int hwnd, int flag)
        {
            return dm.SetWindowState(hwnd, flag);
        }

        public int CreateFoobarRect(int hwnd, int x, int y, int w, int h)
        {
            throw new NotImplementedException();
        }

        public int CreateFoobarRoundRect(int hwnd, int x, int y, int w, int h, int rw, int rh)
        {
            throw new NotImplementedException();
        }

        public int CreateFoobarEllipse(int hwnd, int x, int y, int w, int h)
        {
            throw new NotImplementedException();
        }

        public int CreateFoobarCustom(int hwnd, int x, int y, string pic, string trans_color, double sim)
        {
            throw new NotImplementedException();
        }

        public int FoobarFillRect(int hwnd, int x1, int y1, int x2, int y2, string color)
        {
            throw new NotImplementedException();
        }

        public int FoobarDrawText(int hwnd, int x, int y, int w, int h, string text, string color, int align)
        {
            throw new NotImplementedException();
        }

        public int FoobarDrawPic(int hwnd, int x, int y, string pic, string trans_color)
        {
            throw new NotImplementedException();
        }

        public int FoobarUpdate(int hwnd)
        {
            throw new NotImplementedException();
        }

        public int FoobarLock(int hwnd)
        {
            throw new NotImplementedException();
        }

        public int FoobarUnlock(int hwnd)
        {
            throw new NotImplementedException();
        }

        public int FoobarSetFont(int hwnd, string font_name, int size, int flag)
        {
            throw new NotImplementedException();
        }

        public int FoobarTextRect(int hwnd, int x, int y, int w, int h)
        {
            throw new NotImplementedException();
        }

        public int FoobarPrintText(int hwnd, string text, string color)
        {
            throw new NotImplementedException();
        }

        public int FoobarClearText(int hwnd)
        {
            throw new NotImplementedException();
        }

        public int FoobarTextLineGap(int hwnd, int gap)
        {
            throw new NotImplementedException();
        }

        public int Play(string file)
        {
            throw new NotImplementedException();
        }

        public int FaqCapture(int x1, int y1, int x2, int y2, int quality, int delay, int time)
        {
            throw new NotImplementedException();
        }

        public int FaqRelease(int handle)
        {
            throw new NotImplementedException();
        }

        public string FaqSend(string server, int handle, int request_type, int time_out)
        {
            throw new NotImplementedException();
        }

        public int Beep(int fre, int delay)
        {
            throw new NotImplementedException();
        }

        public int FoobarClose(int hwnd)
        {
            throw new NotImplementedException();
        }

        public int MoveDD(int dx, int dy)
        {
            throw new NotImplementedException();
        }

        public int FaqGetSize(int handle)
        {
            throw new NotImplementedException();
        }

        public int LoadPic(string pic_name)
        {
            throw new NotImplementedException();
        }

        public int FreePic(string pic_name)
        {
            throw new NotImplementedException();
        }

        public int GetScreenData(int x1, int y1, int x2, int y2)
        {
            throw new NotImplementedException();
        }

        public int FreeScreenData(int handle)
        {
            throw new NotImplementedException();
        }

        public int WheelUp()
        {
            throw new NotImplementedException();
        }

        public int WheelDown()
        {
            throw new NotImplementedException();
        }

        public int SetMouseDelay(string type, int delay)
        {
            throw new NotImplementedException();
        }

        public int SetKeypadDelay(string type, int delay)
        {
            throw new NotImplementedException();
        }

        public string GetEnv(int index, string name)
        {
            throw new NotImplementedException();
        }

        public int SetEnv(int index, string name, string value)
        {
            throw new NotImplementedException();
        }

        public int SendString(int hwnd, string str)
        {
            return dm.SendString(hwnd, str);
        }

        public int DelEnv(int index, string name)
        {
            throw new NotImplementedException();
        }

        public string GetPath()
        {
            return dm.GetPath();
        }

        public int SetDict(int index, string dict_name)
        {
            return dm.SetDict(index, dict_name);
        }

        public Point FindPic(int x1, int y1, int x2, int y2, string pic_name, int dir = 0, string delta_color = "", double sim = 0.8)
        {
            object x;
            object y;
            dm.FindPic(x1, y1, x2, y2, pic_name, delta_color, sim, dir, out x, out y);
            return new Point((int)x, (int)y);
        }

        public string FindPicEx(int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir)
        {
            throw new NotImplementedException();
        }

        public int SetClientSize(int hwnd, int width, int height)
        {
            throw new NotImplementedException();
        }

        public int ReadInt(int hwnd, string addr, int type)
        {
            throw new NotImplementedException();
        }

        public float ReadFloat(int hwnd, string addr)
        {
            throw new NotImplementedException();
        }

        public double ReadDouble(int hwnd, string addr)
        {
            throw new NotImplementedException();
        }

        public string FindInt(int hwnd, string addr_range, int int_value_min, int int_value_max, int type)
        {
            throw new NotImplementedException();
        }

        public string FindFloat(int hwnd, string addr_range, float float_value_min, float float_value_max)
        {
            throw new NotImplementedException();
        }

        public string FindDouble(int hwnd, string addr_range, double double_value_min, double double_value_max)
        {
            throw new NotImplementedException();
        }

        public string FindString(int hwnd, string addr_range, string string_value, int type)
        {
            throw new NotImplementedException();
        }

        public int GetModuleBaseAddr(int hwnd, string module_name)
        {
            throw new NotImplementedException();
        }

        public string MoveToEx(int x, int y, int w, int h)
        {
            throw new NotImplementedException();
        }

        public string MatchPicName(string pic_name)
        {
            throw new NotImplementedException();
        }

        public int AddDict(int index, string dict_info)
        {
            throw new NotImplementedException();
        }

        public int EnterCri()
        {
            throw new NotImplementedException();
        }

        public int LeaveCri()
        {
            throw new NotImplementedException();
        }

        public int WriteInt(int hwnd, string addr, int type, int v)
        {
            throw new NotImplementedException();
        }

        public int WriteFloat(int hwnd, string addr, float v)
        {
            throw new NotImplementedException();
        }

        public int WriteDouble(int hwnd, string addr, double v)
        {
            throw new NotImplementedException();
        }

        public int WriteString(int hwnd, string addr, int type, string v)
        {
            throw new NotImplementedException();
        }

        public int AsmAdd(string asm_ins)
        {
            throw new NotImplementedException();
        }

        public int AsmClear()
        {
            throw new NotImplementedException();
        }

        public int AsmCall(int hwnd, int mode)
        {
            throw new NotImplementedException();
        }

        public int FindMultiColor(int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir, out object x, out object y)
        {
            throw new NotImplementedException();
        }

        public string FindMultiColorEx(int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir)
        {
            throw new NotImplementedException();
        }

        public string AsmCode(int base_addr)
        {
            throw new NotImplementedException();
        }

        public string Assemble(string asm_code, int base_addr, int is_upper)
        {
            throw new NotImplementedException();
        }

        public int SetWindowTransparent(int hwnd, int v)
        {
            throw new NotImplementedException();
        }

        public string ReadData(int hwnd, string addr, int len)
        {
            throw new NotImplementedException();
        }

        public int WriteData(int hwnd, string addr, string data)
        {
            throw new NotImplementedException();
        }

        public string FindData(int hwnd, string addr_range, string data)
        {
            throw new NotImplementedException();
        }

        public int SetPicPwd(string pwd)
        {
            throw new NotImplementedException();
        }

        public int Log(string info)
        {
            throw new NotImplementedException();
        }

        public string FindStrE(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public string FindColorE(int x1, int y1, int x2, int y2, string color, double sim, int dir)
        {
            throw new NotImplementedException();
        }

        public string FindPicE(int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir)
        {
            throw new NotImplementedException();
        }

        public string FindMultiColorE(int x1, int y1, int x2, int y2, string first_color, string offset_color, double sim, int dir)
        {
            throw new NotImplementedException();
        }

        public int SetExactOcr(int exact_ocr)
        {
            throw new NotImplementedException();
        }

        public string ReadString(int hwnd, string addr, int type, int len)
        {
            throw new NotImplementedException();
        }

        public int FoobarTextPrintDir(int hwnd, int dir)
        {
            throw new NotImplementedException();
        }

        public string OcrEx(int x1, int y1, int x2, int y2, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public int SetDisplayInput(string mode)
        {
            throw new NotImplementedException();
        }

        public int GetTime()
        {
            throw new NotImplementedException();
        }

        public int GetScreenWidth()
        {
            throw new NotImplementedException();
        }

        public int GetScreenHeight()
        {
            throw new NotImplementedException();
        }

        public int BindWindowEx(int hwnd, string display = "gdi", string mouse = "windows", string keypad = "windows", string public_desc = "", int mode = 0)
        {
            return dm.BindWindowEx(hwnd, display, mouse, keypad, public_desc, mode);
        }

        public string GetDiskSerial()
        {
            throw new NotImplementedException();
        }

        public string Md5(string str)
        {
            throw new NotImplementedException();
        }

        public string GetMac()
        {
            throw new NotImplementedException();
        }

        public int ActiveInputMethod(int hwnd, string id)
        {
            throw new NotImplementedException();
        }

        public int CheckInputMethod(int hwnd, string id)
        {
            throw new NotImplementedException();
        }

        public int FindInputMethod(string id)
        {
            throw new NotImplementedException();
        }

        public Point GetCursorPos()
        {
            object x;
            object y;
            dm.GetCursorPos(out x, out y);
            return new Point((int)x, (int)y);
        }

        public int BindWindow(int hwnd, string display = "gdi", string mouse = "windows", string keypad = "windows", int mode = 0)
        {
            return dm.BindWindow(hwnd, display, mouse, keypad, mode);
        }

        public int FindWindow(string class_name, string title_name)
        {
            return dm.FindWindow(class_name, title_name);
        }

        public int GetScreenDepth()
        {
            return dm.GetScreenDepth();
        }

        public int SetScreen(int width, int height, int depth)
        {
            return dm.SetScreen(width, height, depth);
        }

        public int ExitOs(int type)
        {
            return dm.ExitOs(type);
        }

        public string GetDir(int type)
        {
            return dm.GetDir(type);
        }

        public int GetOsType()
        {
            return dm.GetOsType();
        }

        public int FindWindowEx(int parent, string class_name = "", string title_name = "")
        {
            return dm.FindWindowEx(parent, class_name, title_name);
        }

        public int SetExportDict(int index, string dict_name)
        {
            throw new NotImplementedException();
        }

        public string GetCursorShape()
        {
            return dm.GetCursorShape();
        }

        public int DownCpu(int rate)
        {
            throw new NotImplementedException();
        }

        public string GetCursorSpot()
        {
            throw new NotImplementedException();
        }

        public int SendString2(int hwnd, string str)
        {
            throw new NotImplementedException();
        }

        public int FaqPost(string server, int handle, int request_type, int time_out)
        {
            throw new NotImplementedException();
        }

        public string FaqFetch()
        {
            throw new NotImplementedException();
        }

        public string FetchWord(int x1, int y1, int x2, int y2, string color, string word)
        {
            throw new NotImplementedException();
        }

        public int CaptureJpg(int x1, int y1, int x2, int y2, string file, int quality)
        {
            throw new NotImplementedException();
        }

        public int FindStrWithFont(int x1, int y1, int x2, int y2, string str, string color, double sim, string font_name, int font_size, int flag, out object x, out object y)
        {
            throw new NotImplementedException();
        }

        public string FindStrWithFontE(int x1, int y1, int x2, int y2, string str, string color, double sim, string font_name, int font_size, int flag)
        {
            throw new NotImplementedException();
        }

        public string FindStrWithFontEx(int x1, int y1, int x2, int y2, string str, string color, double sim, string font_name, int font_size, int flag)
        {
            throw new NotImplementedException();
        }

        public string GetDictInfo(string str, string font_name, int font_size, int flag)
        {
            throw new NotImplementedException();
        }

        public int SaveDict(int index, string file)
        {
            throw new NotImplementedException();
        }

        public int GetWindowProcessId(int hwnd)
        {
            throw new NotImplementedException();
        }

        public string GetWindowProcessPath(int hwnd)
        {
            throw new NotImplementedException();
        }

        public int LockInput(int @lock)
        {
            throw new NotImplementedException();
        }

        public string GetPicSize(string pic_name)
        {
            throw new NotImplementedException();
        }

        public int GetID()
        {
            throw new NotImplementedException();
        }

        public int CapturePng(int x1, int y1, int x2, int y2, string file)
        {
            throw new NotImplementedException();
        }

        public int CaptureGif(int x1, int y1, int x2, int y2, string file, int delay, int time)
        {
            throw new NotImplementedException();
        }

        public int ImageToBmp(string pic_name, string bmp_name)
        {
            throw new NotImplementedException();
        }

        public int FindStrFast(int x1, int y1, int x2, int y2, string str, string color, double sim, out object x, out object y)
        {
            throw new NotImplementedException();
        }

        public string FindStrFastEx(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public string FindStrFastE(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public int EnableDisplayDebug(int enable_debug)
        {
            throw new NotImplementedException();
        }

        public int CapturePre(string file)
        {
            throw new NotImplementedException();
        }

        public int RegEx(string code, string Ver, string ip)
        {
            return dm.RegEx(code, Ver, ip);
        }

        public string GetMachineCode()
        {
            return dm.GetMachineCode();
        }

        public int SetClipboard(string data)
        {
            throw new NotImplementedException();
        }

        public string GetClipboard()
        {
            throw new NotImplementedException();
        }

        public int GetNowDict()
        {
            throw new NotImplementedException();
        }

        public int Is64Bit()
        {
            throw new NotImplementedException();
        }

        public int GetColorNum(int x1, int y1, int x2, int y2, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public string EnumWindowByProcess(string process_name, string title, string class_name, int filter)
        {
            throw new NotImplementedException();
        }

        public int GetDictCount(int index)
        {
            throw new NotImplementedException();
        }

        public int GetLastError()
        {
            throw new NotImplementedException();
        }

        public string GetNetTime()
        {
            throw new NotImplementedException();
        }

        public int EnableGetColorByCapture(int en)
        {
            throw new NotImplementedException();
        }

        public int CheckUAC()
        {
            throw new NotImplementedException();
        }

        public int SetUAC(int uac)
        {
            return dm.SetUAC(uac);
        }

        public int DisableFontSmooth()
        {
            return dm.DisableFontSmooth();
        }

        public int CheckFontSmooth()
        {
            return dm.CheckFontSmooth();
        }

        public int SetDisplayAcceler(int level)
        {
            throw new NotImplementedException();
        }

        public int FindWindowByProcess(string process_name, string class_name = "", string title_name = "")
        {
            return dm.FindWindowByProcess(process_name, class_name, title_name);
        }

        public int FindWindowByProcessId(int process_id, string class_name = "", string title_name = "")
        {
            return dm.FindWindowByProcessId(process_id, class_name, title_name);
        }

        public string ReadIni(string section, string key, string file)
        {
            throw new NotImplementedException();
        }

        public int WriteIni(string section, string key, string v, string file)
        {
            throw new NotImplementedException();
        }

        public int RunApp(string path, int mode)
        {
            throw new NotImplementedException();
        }

        public int delay(int mis)
        {
            return dm.delay(mis);
        }

        public int FindWindowSuper(string spec1, int flag1, int type1, string spec2, int flag2, int type2)
        {
            throw new NotImplementedException();
        }

        public string ExcludePos(string all_pos, int type, int x1, int y1, int x2, int y2)
        {
            throw new NotImplementedException();
        }

        public string FindNearestPos(string all_pos, int type, int x, int y)
        {
            throw new NotImplementedException();
        }

        public string SortPosDistance(string all_pos, int type, int x, int y)
        {
            throw new NotImplementedException();
        }

        public int FindPicMem(int x1, int y1, int x2, int y2, string pic_info, string delta_color, double sim, int dir, out object x, out object y)
        {
            throw new NotImplementedException();
        }

        public string FindPicMemEx(int x1, int y1, int x2, int y2, string pic_info, string delta_color, double sim, int dir)
        {
            throw new NotImplementedException();
        }

        public string FindPicMemE(int x1, int y1, int x2, int y2, string pic_info, string delta_color, double sim, int dir)
        {
            throw new NotImplementedException();
        }

        public string AppendPicAddr(string pic_info, int addr, int size)
        {
            throw new NotImplementedException();
        }

        public int WriteFile(string file, string content)
        {
            throw new NotImplementedException();
        }

        public int Stop(int id)
        {
            throw new NotImplementedException();
        }

        public int SetDictMem(int index, int addr, int size)
        {
            throw new NotImplementedException();
        }

        public string GetNetTimeSafe()
        {
            throw new NotImplementedException();
        }

        public int ForceUnBindWindow(int hwnd)
        {
            throw new NotImplementedException();
        }

        public string ReadIniPwd(string section, string key, string file, string pwd)
        {
            throw new NotImplementedException();
        }

        public int WriteIniPwd(string section, string key, string v, string file, string pwd)
        {
            throw new NotImplementedException();
        }

        public int DecodeFile(string file, string pwd)
        {
            throw new NotImplementedException();
        }

        public int KeyDownChar(string key_str)
        {
            throw new NotImplementedException();
        }

        public int KeyUpChar(string key_str)
        {
            throw new NotImplementedException();
        }

        public int KeyPressChar(string key_str)
        {
            throw new NotImplementedException();
        }

        public int KeyPressStr(string key_str, int delay)
        {
            throw new NotImplementedException();
        }

        public int EnableKeypadPatch(int en)
        {
            throw new NotImplementedException();
        }

        public int EnableKeypadSync(int en, int time_out)
        {
            throw new NotImplementedException();
        }

        public int EnableMouseSync(int en, int time_out)
        {
            throw new NotImplementedException();
        }

        public int DmGuard(int en, string type)
        {
            throw new NotImplementedException();
        }

        public int FaqCaptureFromFile(int x1, int y1, int x2, int y2, string file, int quality)
        {
            throw new NotImplementedException();
        }

        public string FindIntEx(int hwnd, string addr_range, int int_value_min, int int_value_max, int type, int step, int multi_thread, int mode)
        {
            throw new NotImplementedException();
        }

        public string FindFloatEx(int hwnd, string addr_range, float float_value_min, float float_value_max, int step, int multi_thread, int mode)
        {
            throw new NotImplementedException();
        }

        public string FindDoubleEx(int hwnd, string addr_range, double double_value_min, double double_value_max, int step, int multi_thread, int mode)
        {
            throw new NotImplementedException();
        }

        public string FindStringEx(int hwnd, string addr_range, string string_value, int type, int step, int multi_thread, int mode)
        {
            throw new NotImplementedException();
        }

        public string FindDataEx(int hwnd, string addr_range, string data, int step, int multi_thread, int mode)
        {
            throw new NotImplementedException();
        }

        public int EnableRealMouse(int en, int mousedelay, int mousestep)
        {
            throw new NotImplementedException();
        }

        public int EnableRealKeypad(int en)
        {
            throw new NotImplementedException();
        }

        public int SendStringIme(string str)
        {
            throw new NotImplementedException();
        }

        public int FoobarDrawLine(int hwnd, int x1, int y1, int x2, int y2, string color, int style, int width)
        {
            throw new NotImplementedException();
        }

        public string FindStrEx(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public int IsBind(int hwnd)
        {
            throw new NotImplementedException();
        }

        public int SetDisplayDelay(int t)
        {
            throw new NotImplementedException();
        }

        public int GetDmCount()
        {
            throw new NotImplementedException();
        }

        public int DisableScreenSave()
        {
            throw new NotImplementedException();
        }

        public int DisablePowerSave()
        {
            throw new NotImplementedException();
        }

        public int SetMemoryHwndAsProcessId(int en)
        {
            throw new NotImplementedException();
        }

        public int FindShape(int x1, int y1, int x2, int y2, string offset_color, double sim, int dir, out object x, out object y)
        {
            throw new NotImplementedException();
        }

        public string FindShapeE(int x1, int y1, int x2, int y2, string offset_color, double sim, int dir)
        {
            throw new NotImplementedException();
        }

        public string FindShapeEx(int x1, int y1, int x2, int y2, string offset_color, double sim, int dir)
        {
            throw new NotImplementedException();
        }

        public string FindStrS(int x1, int y1, int x2, int y2, string str, string color, double sim, out object x, out object y)
        {
            throw new NotImplementedException();
        }

        public string FindStrExS(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public string FindStrFastS(int x1, int y1, int x2, int y2, string str, string color, double sim, out object x, out object y)
        {
            throw new NotImplementedException();
        }

        public string FindStrFastExS(int x1, int y1, int x2, int y2, string str, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public string FindPicS(int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir, out object x, out object y)
        {
            throw new NotImplementedException();
        }

        public string FindPicExS(int x1, int y1, int x2, int y2, string pic_name, string delta_color, double sim, int dir)
        {
            throw new NotImplementedException();
        }

        public int ClearDict(int index)
        {
            throw new NotImplementedException();
        }

        public string GetMachineCodeNoMac()
        {
            throw new NotImplementedException();
        }

        public int GetClientRect(int hwnd, out object x1, out object y1, out object x2, out object y2)
        {
            throw new NotImplementedException();
        }

        public int EnableFakeActive(int en)
        {
            throw new NotImplementedException();
        }

        public int GetScreenDataBmp(int x1, int y1, int x2, int y2, out object data, out object size)
        {
            throw new NotImplementedException();
        }

        public int EncodeFile(string file, string pwd)
        {
            throw new NotImplementedException();
        }

        public string GetCursorShapeEx(int type)
        {
            throw new NotImplementedException();
        }

        public int FaqCancel()
        {
            throw new NotImplementedException();
        }

        public string IntToData(int int_value, int type)
        {
            throw new NotImplementedException();
        }

        public string FloatToData(float float_value)
        {
            throw new NotImplementedException();
        }

        public string DoubleToData(double double_value)
        {
            throw new NotImplementedException();
        }

        public string StringToData(string string_value, int type)
        {
            throw new NotImplementedException();
        }

        public int SetMemoryFindResultToFile(string file)
        {
            throw new NotImplementedException();
        }

        public int EnableBind(int en)
        {
            throw new NotImplementedException();
        }

        public int SetSimMode(int mode)
        {
            throw new NotImplementedException();
        }

        public int LockMouseRect(int x1, int y1, int x2, int y2)
        {
            throw new NotImplementedException();
        }

        public int SendPaste(int hwnd)
        {
            throw new NotImplementedException();
        }

        public int IsDisplayDead(int x1, int y1, int x2, int y2, int t)
        {
            throw new NotImplementedException();
        }

        public int GetKeyState(int vk)
        {
            throw new NotImplementedException();
        }

        public int CopyFile(string src_file, string dst_file, int over)
        {
            throw new NotImplementedException();
        }

        public int IsFileExist(string file)
        {
            throw new NotImplementedException();
        }

        public int DeleteFile(string file)
        {
            throw new NotImplementedException();
        }

        public int MoveFile(string src_file, string dst_file)
        {
            throw new NotImplementedException();
        }

        public int CreateFolder(string folder_name)
        {
            throw new NotImplementedException();
        }

        public int DeleteFolder(string folder_name)
        {
            throw new NotImplementedException();
        }

        public int GetFileLength(string file)
        {
            throw new NotImplementedException();
        }

        public string ReadFile(string file)
        {
            throw new NotImplementedException();
        }

        public int WaitKey(int key_code, int time_out)
        {
            throw new NotImplementedException();
        }

        public int DeleteIni(string section, string key, string file)
        {
            throw new NotImplementedException();
        }

        public int DeleteIniPwd(string section, string key, string file, string pwd)
        {
            throw new NotImplementedException();
        }

        public int EnableSpeedDx(int en)
        {
            throw new NotImplementedException();
        }

        public int EnableIme(int en)
        {
            throw new NotImplementedException();
        }

        public int Reg(string code, string Ver)
        {
            throw new NotImplementedException();
        }

        public string SelectFile()
        {
            throw new NotImplementedException();
        }

        public string SelectDirectory()
        {
            throw new NotImplementedException();
        }

        public int LockDisplay(int @lock)
        {
            throw new NotImplementedException();
        }

        public int FoobarSetSave(int hwnd, string file, int en, string header)
        {
            throw new NotImplementedException();
        }

        public string EnumWindowSuper(string spec1, int flag1, int type1, string spec2, int flag2, int type2, int sort)
        {
            throw new NotImplementedException();
        }

        public int DownloadFile(string url, string save_file, int timeout)
        {
            throw new NotImplementedException();
        }

        public int EnableKeypadMsg(int en)
        {
            throw new NotImplementedException();
        }

        public int EnableMouseMsg(int en)
        {
            throw new NotImplementedException();
        }

        public int RegNoMac(string code, string Ver)
        {
            throw new NotImplementedException();
        }

        public int RegExNoMac(string code, string Ver, string ip)
        {
            throw new NotImplementedException();
        }

        public int SetEnumWindowDelay(int delay)
        {
            throw new NotImplementedException();
        }

        public int FindMulColor(int x1, int y1, int x2, int y2, string color, double sim)
        {
            throw new NotImplementedException();
        }

        public string GetDict(int index, int font_index)
        {
            throw new NotImplementedException();
        }
    }
}
