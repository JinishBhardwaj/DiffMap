<b>The Solution has following projects:</b>
- DiffService: Contains the V1 and V2 api for accepting data and providing diff results
- Common: Contains classes common to all projects
- ConsoleClient: Provides integration wih the DiffService to test the api endpoints

<b>Unit Tests:</b>
- DiffServiceTests: Contains the tests for controllers, DiffChecked algorithm and ThreadSafeDictionary implementation
- CommonTests: Contains unit tests for TupleConverter


<b>DiffService:</b>
Provides 2 Api controllers
- <b>DiffV1Controller</b>
  - Endpoints (v1/diff/left and v1/diff/right) accepts byte[] as the content and provides the result on the endpoint (v1/diff)
- <b>DiffV2Controller</b>
  - Endpoints (v2/diff/left and v2/diff/right) accepts Base64 encoded strings as the content and provides the results on endpoint (v2/diff)


<b>DiffChecker class:</b>

- Has 2 overloads of the GetDiff() method accepting byte[] and Base64 encoded string
- If the 2 provided byte[]/ Base64 strings are of equal length then it performs a BITWISE XOR (^) on each corresponding byte of both the provided arrays and stores the result in the Response container where the result is not 0

<b>Other considered but not implemented ways of calculating the diff elements:</b>

-<b>Approach 1:</b>
  - var diff = _streams["Left"].Select((x, y) => new { x, y }).Join(_streams["Right"].Select((x, y) => new { x, y }), x => x.y, x => x.y, (d1, d2) => Math.Abs(d1.x - d2.x)).ToArray();
  
-<b>Approach:</b>
  - var diff = _streams["Left"].Zip(_streams["Right"], (d1, d2) => Math.Abs(d1 - d2)).ToArray();

Issue with both these approaches is that the ToArray() function internally has to Resize the Array to accomodate for the lengths and hence involves extra overhead that can result in slow performance for larger sets of data

