# com.unity.xrtools.mars-viking
Named after NASA's Viking project which became the first US mission to land a spacecraft on the surface of MARS and return photographs of the surface.

## Setup
1. Create a new Unity project
2. Clone this repository and add it to the Packages folder of the project you created in step 1
3. Add the package name to the "Testables" list in the project's manifest.json
4. Add the candidates registry to the project's manifest.json

```
{
  "dependencies": {
      ...
  },
  "testables": ["com.unity.xrtools.mars-viking"],
  "registry": "https://artifactory.prd.cds.internal.unity3d.com/artifactory/api/npm/upm-candidates"
}
```

## Writing Test Cases
### Nunit Attributes and TestRail
Each test should have a one-to-one mapping to the tests in [TestRail](http://qatestrail.hq.unity3d.com/index.php?/suites/view/5708).  You should add the TestRail test id to the Attributes of your test case :

```
[NUnit.Framework.Property("TestRailId", "C576069")]
```

Each test should also make use of the Category Nunit Attribute so that we can run collections of tests under different release scenerios.  The category should match the TestRail `Type` field of the test.

```
[Category("Acceptance")]
```

### Constants
Each test should make use of constants for location paths, text and any other GUI Elements used in a test.  The idea here is to have one location for all strings which can be reused.  It also makes modifications easy when a change takes place in the application under test.

## Running Tests
Running tests should be done by using the UTR standalone.

### Download UTR
The tool can be downloaded by using the script found in (this)[https://github.cds.internal.unity3d.com/unity/utr] repository.  Instructions can be found in the README.

### Run locally
```
utr --suite=editor --suite=playmode --testproject=/my/project/path --editor-location=/Applications/Unity/Hub/Editor/2019.3.0f6/ --artifacts_path=/my/test/results/path
```