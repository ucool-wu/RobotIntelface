# RobotIntelface
The unofficial implement of fanuc robot-intelface
# test on
R-30ib mate robot controller
# how to use the lib
1.Instantiate : FanucRobIntelface fi = new FanucRobIntelface("192.168.1.100");
2.Read data : call fi.Refresh(),then read the data properties such as fi.intRegs. Whenever to get data, the refresh function must be called.
3.write data : call write functions such as fi.WriteR().
