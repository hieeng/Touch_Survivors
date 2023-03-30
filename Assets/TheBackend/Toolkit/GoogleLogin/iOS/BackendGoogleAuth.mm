#import <UIKit/UIKit.h>
#import <GoogleSignIn/GoogleSignIn.h>
typedef void (*backendgoogleDelegate)(bool, const char*, const char*);

@interface BackendFederation: UIResponder <UIApplicationDelegate, GIDSignInDelegate>
{
}

@property (assign) backendgoogleDelegate backendGoogleLoginHandler;
@end

@implementation BackendFederation

static BackendFederation *instance;

+(BackendFederation*) instance
{
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[BackendFederation alloc] init];
    });
    return instance;
}

-(id)init
{
    self = [super init];
    if (self)
        NSLog(@"BackendGoogleAuth was created");
    return self;
}


-(void)startGoogleLogin:(NSString*)_webClientId
{
    [GIDSignIn sharedInstance].presentingViewController = UnityGetGLViewController();
    [GIDSignIn sharedInstance].clientID = _webClientId;
    [GIDSignIn sharedInstance].delegate = self;
    [[GIDSignIn sharedInstance] signIn];
}


- (void)signIn:(GIDSignIn *)signIn
didSignInForUser:(GIDGoogleUser *)user
     withError:(NSError *)error {
  if (error != nil) {
    if (error.code == kGIDSignInErrorCodeHasNoAuthInKeychain) {
      NSLog(@"The user has not signed in before or they have since signed out.");
    }
      NSString *errorMessage = [@"GoogleLogin Failed. Reason" stringByAppendingString:error.localizedDescription];

    self.backendGoogleLoginHandler(false, [self charToString:errorMessage], "");

    return;
  }
    
  NSString *idToken = user.authentication.idToken;
  self.backendGoogleLoginHandler(true, "", [self charToString:idToken]);
    
}

-(char *) charToString:(NSString *)string
{
    const char* stringUTF8 = [string UTF8String];
    char* stringChar = (char*)malloc(strlen(stringUTF8) + 1);
    strcpy(stringChar, stringUTF8);
    
    return stringChar;
}

extern "C"
{
    void StartGoogleLogin(const char *webClientId, backendgoogleDelegate handler)
    {
        [BackendFederation instance].backendGoogleLoginHandler = handler;
        [[BackendFederation instance] startGoogleLogin:[NSString stringWithUTF8String:webClientId]];
    }
}
@end
