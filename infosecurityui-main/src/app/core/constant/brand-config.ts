/**
 * Brand Configuration Constants
 * Centralized configuration for brand-related constants
 */

// 4️⃣ Single source of truth (Base URL)
const BRAND_BASE_URL = 'https://aspire.tenarai.com/';

export const BRAND_CONFIG = {
  companyName: 'Tenarai',
  companyShortName: 'Tenarai',
  
  logo: {
    primary: `https://aspire.tenarai.com/content/brand/Logo-white151x55.png`,
    secondary: 'assets/images/Infogain_Logo.png',
    login: `https://aspire.tenarai.com/content/brand/Logo-white151x55.png`,
    width: '180px',
    alt: 'Tenarai Logo'
  },
  
  images: {
    error: 'assets/images/error.png',
    errorWidth: '200px',
    thankYou: 'assets/images/thank-you.png'
  },
  
  copyright: {
    text: '© 2026 Tenarai Inc. All rights reserved.',
    shortText: '© 2026 Tenarai Inc. All rights reserved.',
    year: new Date().getFullYear()
  },
  
  routes: {
    home: '/login',
    error: '/error',
    unauthorized: '/not-authorized'
  },
  
  contact: {
    cisofunctionEmail: 'CISOFunction@tenarai.com',
    serviceDeskUrl: 'https://servicedesk.tenarai.com/',
    serviceDeskText: 'Tenarai Service Helpdesk',
    DPOEmail: 'mailto:DPO@tenarai.com'
  },
  
  messages: {
    errorGeneric: 'Something went wrong while displaying this page, Pls Connect with System Admin.',
    accessDenied: 'We appreciate your understanding, it seems you are not eligible to use this application currently.',
    haveWonderfulTime: 'Have a wonderful time!',
    unauthorizedUser: 'You are not an Authorized User'
  }
} as const;

/**
 * Application-wide constants
 */
export const APP_CONSTANTS = {
  appName: 'InfoSecurity',
  defaultPageTitle: 'InfoSecurity',
  pageTitle: {
    login: 'Data Security and Data Privacy',
    notAuthorized: 'NotAuthorized',
    survey: 'People Support Experience Survey (PSES)',
    lms: 'LMS'
  },
  
  // API related constants
  api: {
    timeout: 30000,
    retryAttempts: 3
  },
  
  // Session related constants
  session: {
    tokenKey: 'auth_token',
    userKey: 'user_details',
    expiryKey: 'token_expiry'
  }
} as const;
